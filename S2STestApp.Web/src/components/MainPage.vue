<template>
  <div>
    <Header />
    <div class="main-page__row">
      <cart
        :itemsCount="cartItemsCount"
        v-on:itemDeleted="cartItemsCountChanged"
        v-on:modalClosed="modalClosed"
      />
    </div>
    <books-list
      v-on:cartItemsCountChanged="cartItemsCountChanged"
      ref="booksList"
    />
  </div>
</template>


<script lang="ts">
import { defineComponent } from "vue";
import Header from "./Header.vue";
import BooksList from "./BooksList.vue";
import Cart from "./Cart.vue";

export default defineComponent({
  name: "MainPage",
  components: { Header, BooksList, Cart },
  data: function () {
    return {
      cartItemsCount: 0,
    };
  },
  methods: {
    cartItemsCountChanged(count: Number) {
      this.cartItemsCount = count;
      //this.$refs.booksList.loadPageData();
    },

    modalClosed() {
      this.$refs.booksList.loadPageData();
    },
  },
});
</script>

<style scoped>
.main-page__row {
  display: flex;
  align-items: center;
  justify-content: flex-end;
}
</style>