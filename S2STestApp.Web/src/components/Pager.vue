<template>
  <div class="pager">
    <button
      class="pager__btn"
      :class="{ pager__btn_disabled: prevDisabled() }"
      v-on:click="prevPageClicked"
      :disabled="prevDisabled()"
    >
      <span class="pager__icon">&#10094;</span>
    </button>

    <span class="pager__txt">
      <span v-if="!loading">
        Страница <span v-text="getCurrentPage()"></span> из
        <span v-text="getPageCount()"></span>
      </span>

      <loader :loading="this.loading" />
    </span>

    <button
      class="pager__btn"
      v-on:click="nextPageClicked"
      :class="{ pager__btn_disabled: nextDisabled() }"
      :disabled="nextDisabled()"
    >
      <span class="pager__icon">&#10095;</span>
    </button>
  </div>
</template>
<script>
import Loader from "./Loader.vue";
export default {
  components: { Loader },
  props: {
    loading: {
      type: Boolean,
      default: false,
    },

    pageCount: Number,
    sortKey: String,
  },

  data: function () {
    return {
      currentPage: 0,
    };
  },

  created() {},

  methods: {
    getCurrentPage() {
      return this.currentPage + 1;
    },
    getPageCount() {
      return this.pageCount;
    },

    prevDisabled() {
      return this.loading || this.currentPage === 0;
    },

    nextDisabled() {
      return this.loading || this.currentPage === this.pageCount - 1;
    },
    prevPageClicked() {
      this.$emit("pageChanged", this.currentPage - 1);
      this.currentPage = this.currentPage - 1;
    },

    nextPageClicked() {
      let nextPage = this.currentPage + 1;
      this.$emit("pageChanged", nextPage);
      this.currentPage = nextPage;
    },
  },

  watch: {
    sortKey(newVal, oldVal) {
      if (newVal != oldVal) {
        this.currentPage = 0;
      }
    },

    pageCount(newVal, oldVal) {
      if (oldVal > newVal) {
        this.currentPage = newVal - 1;
      }
    },
  },
};
</script>

<style scoped>
.pager {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 80px;
}

.pager__btn {
  background-color: transparent;
  border-radius: 5px;
  font-family: Golos;
  font-weight: 400;
  font-size: 27px;
  width: 50px;
  cursor: pointer;
  transition: all 0.4s;
  opacity: 0.6;
}

.pager__btn_disabled {
  cursor: not-allowed;
  opacity: 0.2;
}

.pager__btn:hover {
  opacity: 1;
}

.pager__txt {
  margin-left: 10px;
  margin-right: 10px;
  width: 120px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.pager__icon {
  transform: scale(3);
}
</style>