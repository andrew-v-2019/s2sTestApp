<template>
  <div class="icon-cart" v-on:click="cartClicked">
    <div class="cart-line-1" style="background-color: #2cc3b5"></div>
    <div class="cart-line-2" style="background-color: #2cc3b5"></div>
    <div class="cart-line-3" style="background-color: #2cc3b5"></div>
    <div class="cart-wheel" style="background-color: #2cc3b5"></div>
  </div>

  <div
    class="cart__badge"
    v-if="itemsCount"
    :class="{ cart__badge_highlight: highlight }"
  >
    <span>{{ itemsCount }}</span>
  </div>

  <cart-modal
    :visible="modalVisible"
    v-on:close="cartClosed"
    v-on:itemDeleted="itemDeleted"
  />
</template>
<script lang="ts">
import { defineComponent } from "vue";
import CartModal from "./CartModal.vue";

export default defineComponent({
  components: { CartModal },
  name: "Cart",
  props: {
    itemsCount: Number,
  },

  emits: ["itemDeleted", "modalClosed"],

  data: function () {
    return {
      highlight: false,
      modalVisible: false,
    };
  },

  created() {},

  methods: {
    itemDeleted(newItemsCount: Number) {
      this.$emit("itemDeleted", newItemsCount);
    },
    cartClosed() {
      this.modalVisible = false;
      this.$emit("modalClosed");
    },
    highlightBage() {
      this.highlight = true;
      setTimeout(() => {
        this.highlight = false;
      }, 500);
    },

    cartClicked() {
      this.modalVisible = true;
    },
  },

  watch: {
    itemsCount: function (newVal, oldVal) {
      if (newVal > 1 && oldVal === 0) {
        return;
      }
      this.highlightBage();
    },
  },
});
</script>

<style scoped>
.cart__badge {
  font-weight: bold;
  width: 20px;
  height: 20px;
  padding: 3px;
  font-size: 19px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 100%;
  background-color: rgba(255, 0, 40, 0.5);
  color: black;
  position: absolute;
  top: 50px;
  right: 5px;
  transition: all 0.3s;
  border: 2px solid rgba(164, 0, 0, 1);
}

.cart__badge_highlight {
  background-color: rgba(255, 0, 40, 1);
  border: 3px solid rgb(164, 0, 0, 1);
  transform: scale(1.5);
}

.icon-cart {
  clear: left;
  float: left;
  width: 90px;
  height: 90px;
  position: relative;
  overflow: hidden;
  cursor: pointer;
  transition: all 0.3s;
}

.icon-cart:hover {
  transform: scale(1.3);
}

.icon-cart:hover + .cart__badge {
  background-color: rgba(255, 0, 40, 1);
}

.icon-cart .cart-line-1 {
  width: 15%;
  height: 7%;
  position: absolute;
  left: 8%;
  top: 25%;
  -webkit-transform: rotate(5deg);
  -moz-transform: rotate(5deg);
  -ms-transform: rotate(5deg);
  transform: rotate(5deg);
  background-color: #000;
  border-bottom-left-radius: 35%;
}

.icon-cart .cart-line-2 {
  width: 35%;
  height: 7%;
  position: absolute;
  left: 6%;
  top: 40%;
  -webkit-transform: rotate(80deg);
  -moz-transform: rotate(80deg);
  -ms-transform: rotate(80deg);
  transform: rotate(80deg);
  background-color: #000;
}

.icon-cart .cart-line-2:before {
  content: "";
  width: 120%;
  height: 100%;
  position: absolute;
  left: 45%;
  top: -280%;
  -webkit-transform: rotate(-80deg);
  -moz-transform: rotate(-80deg);
  -ms-transform: rotate(-80deg);
  transform: rotate(-80deg);
  background-color: inherit;
}

.icon-cart .cart-line-2:after {
  content: "";
  width: 70%;
  height: 100%;
  position: absolute;
  left: 59%;
  top: -670%;
  background-color: inherit;
  -webkit-transform: rotate(40deg);
  -moz-transform: rotate(40deg);
  -ms-transform: rotate(40deg);
  transform: rotate(40deg);
  border-top-left-radius: 50%;
  border-bottom-left-radius: 25%;
}

.icon-cart .cart-line-3 {
  width: 30%;
  height: 7%;
  position: absolute;
  left: 33%;
  top: 45%;
  background-color: #000;
}

.icon-cart .cart-line-3:after {
  content: "";
  width: 124%;
  height: 100%;
  position: absolute;
  top: -150%;
  left: -5%;
  background-color: inherit;
}

.icon-cart .cart-wheel {
  width: 12%;
  height: 12%;
  background-color: #000;
  border-radius: 100%;
  position: absolute;
  left: 28%;
  bottom: 20%;
}

.icon-cart .cart-wheel:after {
  content: "";
  width: 100%;
  height: 100%;
  background-color: inherit;
  border-radius: 100%;
  position: absolute;
  left: 200%;
  bottom: 0;
}
</style>