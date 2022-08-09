<template>
  <modal-window :open="visible" :width="`400px`" v-on:close="close">
    <template v-slot:header>Корзина</template>
    <template v-slot:body>
      <div class="cart-modal__wrapper">
        <table v-if="!loading && cartItems.length" class="cart-modal__table">
          <thead>
            <tr>
              <th width="10%"></th>
              <th width="90%">Книга</th>
            </tr>
          </thead>
          <tbody>
            <tr :key="cartItem.cartItemId" v-for="cartItem in cartItems">
              <td>
                <loader-button
                  :loading="loadingItemId === cartItem.cartItemId"
                  :text="`&#10006;`"
                  color="red"
                  :disabled="this.loading || loadingItemId > 0"
                  v-on:click="cancelClicked(cartItem)"
                />
              </td>
              <td>{{ cartItem.bookName }}</td>
            </tr>
          </tbody>
        </table>

        <div
          v-if="!loading && !cartItems.length"
          class="cart-modal__empty-text"
        >
          Пусто
        </div>

        <div v-if="loading" class="cart-modal__empty-text">
          <loader :loading="loading" />
        </div>
      </div>
    </template>
  </modal-window>
</template>

<script lang="ts">
import { CartItem } from "../models/cartItem";
import { defineComponent } from "vue";
import ModalWindow from "./ModalWindow.vue";
import { CartService } from "../services/cartService";
import Loader from "./Loader.vue";
import LoaderButton from "./LoaderButton.vue";

let cartService = new CartService();

export default defineComponent({
  components: { ModalWindow, Loader, LoaderButton },
  name: "CartModal",
  props: {
    visible: Boolean,
  },

  emits: ["itemDeleted", "close"],

  data: function () {
    return {
      cartItems: new Array<CartItem>(),
      loading: true,
      loadingItemId: 0,
    };
  },
  created() {},

  methods: {
    async cancelClicked(cartItem: CartItem) {
      this.loadingItemId = cartItem.cartItemId;
      let updatedCartItems = await cartService.cancel(cartItem.cartItemId);
      this.cartItems = updatedCartItems;
      this.loadingItemId = 0;

      this.$emit("itemDeleted", this.cartItems.length);
    },
    close() {
      this.$emit("close");
    },
    async load() {
      this.loading = true;
      let cartItems = await cartService.getCart();
      this.cartItems = cartItems;
      this.loading = false;
    },
  },

  watch: {
    visible: function (newVal, oldVal) {
      if (newVal) {
        this.load();
      } else {
        this.cartItems = [];
      }
    },
  },
});
</script>

<style scoped>
.cart-modal__table {
  color: #0c093e;
  font-family: "Golos", Arial, sans-serif;
  line-height: 1.15;
  font-weight: 400;
  width: 100%;
  border-collapse: collapse;
  transition: all 0.3s;
  text-align: center;
  font-size: 15px;
  vertical-align: middle;
}

.cart-modal__table td {
  padding: 10px;
  border-collapse: collapse;
  border-right: 1px solid rgba(211, 211, 211, 0.2);
}

.cart-modal__table tbody tr:nth-child(odd) {
  background-color: #f2f2f2;
}

.cart-modal__table tbody tr:hover {
  background: rgb(211, 211, 211);
}

.cart-modal__wrapper {
  min-height: 100px;
  overflow-y: auto;
  width: 100%;
  overflow-y: auto;
  height: 300px;
  overflow-x: hidden;
  margin-bottom: 30px;
}

.cart-modal__empty-text {
  font-weight: 700;
  opacity: 0.3;
  font-size: 20px;
  width: 100%;
  display: flex;
  height: 100%;
  align-items: center;
  justify-content: center;
}
</style>
