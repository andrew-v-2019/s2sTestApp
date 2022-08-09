<template>
  <div class="books-list">
    <table
      class="books-list__table"
      v-if="!firstLoading"
      :class="{ 'books-list__table_disabled': loading }"
    >
      <thead class="books-list__table-head">
        <tr>
          <th width="10%" class="books-list__table-cell"></th>
          <th width="40%" class="books-list__table-cell">
            <div class="books-list__head-contetnt">
              <span
                class="books-list__head-title"
                v-on:click="headerClicked(`name`)"
                :class="{
                  'books-list__head-title_active': isActiveSortField(`name`),
                }"
              >
                Книга</span
              >
              <span class="books-list__head-actions">
                <span
                  class="books-list__sort-icon books-list__sort-up"
                  :class="{
                    'books-list__sort-icon_active': isActiveSortIcon(`name`, 1),
                  }"
                  v-on:click="sortClicked(`name`, 1)"
                >
                  &#9650;
                </span>
                <span
                  class="books-list__sort-icon books-list__sort-down"
                  :class="{
                    'books-list__sort-icon_active': isActiveSortIcon(`name`, 2),
                  }"
                  v-on:click="sortClicked(`name`, 2)"
                >
                  &#9660;
                </span>
              </span>
            </div>
          </th>
          <th width="40%" class="books-list__table-cell">
            <div class="books-list__head-contetnt">
              <span
                class="books-list__head-title"
                v-on:click="headerClicked(`author`)"
                :class="{
                  'books-list__head-title_active': isActiveSortField(`author`),
                }"
              >
                Автор</span
              >
              <span class="books-list__head-actions">
                <span
                  class="books-list__sort-icon books-list__sort-up"
                  :class="{
                    'books-list__sort-icon_active': isActiveSortIcon(
                      `author`,
                      1
                    ),
                  }"
                  v-on:click="sortClicked(`author`, 1)"
                >
                  &#9650;
                </span>
                <span
                  class="books-list__sort-icon books-list__sort-down"
                  :class="{
                    'books-list__sort-icon_active': isActiveSortIcon(
                      `author`,
                      2
                    ),
                  }"
                  v-on:click="sortClicked(`author`, 2)"
                >
                  &#9660;
                </span>
              </span>
            </div>
          </th>
          <th width="10%" class="books-list__table-cell">
            <div class="books-list__head-contetnt">
              <span
                class="books-list__head-title"
                v-on:click="headerClicked(`year`)"
                :class="{
                  'books-list__head-title_active': isActiveSortField(`year`),
                }"
              >
                Год</span
              >
              <span class="books-list__head-actions">
                <span
                  class="books-list__sort-icon books-list__sort-up"
                  v-on:click="sortClicked(`year`, 1)"
                  :class="{
                    'books-list__sort-icon_active': isActiveSortIcon(`year`, 1),
                  }"
                >
                  &#9650;
                </span>
                <span
                  class="books-list__sort-icon books-list__sort-down"
                  v-on:click="sortClicked(`year`, 2)"
                  :class="{
                    'books-list__sort-icon_active': isActiveSortIcon(`year`, 2),
                  }"
                >
                  &#9660;
                </span>
              </span>
            </div>
          </th>
        </tr>
      </thead>
      <tbody class="books-list__table-body">
        <tr :key="book.id" v-for="book in books">
          <td class="books-list__table-cell">
            <div class="books_list__actions-wrap">
              <loader-button
                :loading="loadingBookId === book.id"
                :text="`Купить`"
                :disabled="this.loading"
                v-on:click="buyClicked(book)"
              />
            </div>
          </td>
          <td v-text="book.name" class="books-list__table-cell"></td>
          <td v-text="book.author" class="books-list__table-cell"></td>
          <td class="books-list__table-cell">
            {{ book.year < 0 ? `${book.year * -1} BC` : book.year }}
          </td>
        </tr>
      </tbody>
      <tfoot>
        <tr>
          <td colspan="4">
            <pager
              :loading="loading"
              :pageCount="pageCount"
              v-on:pageChanged="pageChanged"
              :sortKey="sortKey"
            />
          </td>
        </tr>
      </tfoot>
    </table>

    <loader :loading="firstLoading" />
  </div>
</template>


<script lang="ts">
import { Book } from "@/models/book";
import { BooksRequest } from "@/models/booksRequest";
import { SortDirection } from "@/models/sortDirection";
import { BooksService } from "@/services/bookService";
import { CartService } from "@/services/cartService";
import { defineComponent } from "vue";
import Loader from "./Loader.vue";
import LoaderButton from "./LoaderButton.vue";
import Pager from "./Pager.vue";

let booksService = new BooksService();
let cartService = new CartService();

export default defineComponent({
  name: "BooksList",
  components: { Loader, Pager, LoaderButton },
  data() {
    return {
      books: new Array<Book>(),
      pageCount: 0,
      firstLoading: true,
      loading: false,

      sortField: `id`,
      sortDirection: SortDirection.None,
      sortKey: `id-None`,
      loadingBookId: null,
      cartItemsCount: 0,
      pageNumber: 0,
      userData: String,
    };
  },
  async created() {
    this.firstLoading = true;
    await this.loadPageData();
    this.firstLoading = false;
  },

  methods: {
    async buyClicked(book: Book) {
      this.loadingBookId = book.id;
      this.loading = true;
      await cartService.buy(book.id);

      await this.loadPageData();
      this.loading = false;
      if (this.books.length === 0) {
        this.pageChanged(this.pageNumber - 1);
      }

      this.loadingBookId = null;
    },

    async loadPageData() {
      let request = this.getCurrentBooksRequest();

      let pageData = await booksService.getPageData(request);

      this.books = pageData.books;
      this.pageCount = pageData.pageCount;
      this.cartItemsCount = pageData.cartItemsCount;

      this.$emit("cartItemsCountChanged", this.cartItemsCount);

      this.userData = pageData.userData;
    },

    headerClicked(name: String) {
      if (this.loading) {
        return;
      }
      if (this.sortField === name && this.sortDirection === SortDirection.Asc) {
        this.sortDirection = SortDirection.Desc;
      } else if (
        this.sortField === name &&
        this.sortDirection === SortDirection.Desc
      ) {
        this.sortField = `id`;
        this.sortDirection = SortDirection.None;
      } else {
        this.sortField = name;
        this.sortDirection = SortDirection.Asc;
      }

      this.sortKey = `${name}-${this.sortDirection}`;
      this.pageNumber = 0;
      this.load();
    },

    isActiveSortIcon(name: string, sort: SortDirection) {
      return this.sortField === name && this.sortDirection === sort;
    },
    isActiveSortField(name: String) {
      return this.sortField === name;
    },
    async sortClicked(name: string, sort: SortDirection) {
      if (this.loading) {
        return;
      }

      if (this.sortField === name && this.sortDirection === sort) {
        this.sortField = `id`;
        this.sortDirection = SortDirection.None;
        this.sortKey = `id-None`;
      } else {
        this.sortField = name;
        this.sortDirection = sort;
        this.sortKey = `${name}-${sort}`;
      }

      this.pageNumber = 0;
      this.load();
    },

    async pageChanged(nextPageNum: number) {
      if (this.loading) {
        return;
      }

      this.pageNumber = nextPageNum;

      this.load();
    },

    guid() {
      return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
        /[xy]/g,
        function (c) {
          var r = (Math.random() * 16) | 0,
            v = c == "x" ? r : (r & 0x3) | 0x8;
          return v.toString(16);
        }
      );
    },

    getUserId() {
      debugger;
      if (!localStorage.getItem("s2suserId")) {
        localStorage.setItem("s2suserId", this.guid());
      }

      let userId = localStorage.getItem("s2suserId");
      return userId;
    },

    getCurrentBooksRequest() {
      let request = new BooksRequest(
        this.pageNumber,
        this.sortField,
        this.sortDirection,
        this.getUserId()
      );

      return request;
    },

    async load() {
      this.loading = true;
      let request = this.getCurrentBooksRequest();

      let newBooks = await booksService.getBooks(request);

      this.books = newBooks;
      this.loading = false;
    },
  },
});
</script>

<style scoped>
.books-list__table-cell {
  padding: 10px;
  border-collapse: collapse;
  border-right: 1px solid rgba(211, 211, 211, 0.2);
}
.books-list {
  background: #ffffff;
  padding: 10px;
  display: flex;
  align-items: center;
  justify-content: center;

  min-height: 100px;
}

.books-list__table-head {
  font-size: 24px;
}

.books-list__table-body {
  font-size: 16px;
  text-align: center;
  vertical-align: middle;
}

.books-list__table {
  color: #0c093e;

  font-family: "Golos", Arial, sans-serif;
  line-height: 1.15;
  font-weight: 400;
  width: 100%;
  border-collapse: collapse;
  transition: all 0.3s;
}

.books-list__table_disabled {
  opacity: 0.3;
}

.books-list__table thead tr {
  border-bottom: 1px solid rgba(211, 211, 211, 0.2);
  border-top: 1px solid rgba(211, 211, 211, 0.1);
}

.books-list__table-body tr:nth-child(odd) {
  background-color: #f2f2f2;
}

.books-list__table tr {
  transition: all 0.4s;
}

.books-list__table tr:hover {
  background: rgb(211, 211, 211);
}

.books-list__head-contetnt {
  display: flex;
  align-items: center;
  justify-content: center;
}

.books-list__head-actions {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  font-size: 15px;
}

.books-list__head-title {
  flex: 1 1 auto;
  opacity: 0.5;
  transition: all 0.4s;
  cursor: pointer;
}

.books-list__head-title_active {
  opacity: 1 !important;
}

.books-list__head-title:hover {
  opacity: 0.9;
}

.books-list__sort-icon {
  opacity: 0.5;
  transition: all 0.4s;
  cursor: pointer;
}

.books-list__sort-icon:hover {
  opacity: 1;
  transform: scaleX(1.2);
}

.books-list__sort-icon_active {
  transform: scaleX(1.5);
  opacity: 1;
}

.books_list__actions-wrap {
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>