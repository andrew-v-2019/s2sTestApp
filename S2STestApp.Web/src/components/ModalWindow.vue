<template>
  <transition name="analitics-trans">
    <div class="modal-window__modal-mask" v-if="showModal">
      <div class="modal-window__modal-wrapper">
        <div
          class="modal-window__modal-container"
          v-bind:style="{
            width: width,
          }"
        >
          <div class="modal-window__modal-header">
            <div class="modal-window__modal-header-text">
              <slot name="header"> Заголовок</slot>
            </div>

            <div class="modal-window__modal-header-close" @click="close">×</div>
          </div>

          <div class="modal-window__modal-body">
            <slot name="body"> Содержимое </slot>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    open: Boolean,
    width: {
      type: String,
      default: `300px`,
    },
  },

  data() {
    return {
      showModal: false,
    };
  },

  methods: {
    close() {
      this.showModal = false;
      this.$emit("close");
    },
  },

  watch: {
    open(newVal) {
      this.showModal = newVal;
    },
  },
});
</script>

<style scoped>
.modal-window__modal-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-bottom: 1px solid #dee2e6;
  padding: 6px 15px 6px 15px;
}

.modal-window__modal-mask {
  position: fixed;
  z-index: 3;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: all 0.3s ease;
}

.modal-window__modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-window__modal-container {
  margin: 0px auto;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
  font-family: Helvetica, Arial, sans-serif;
}

.analitics-trans-enter-active,
.analitics-trans-leave-active {
  transition: all 0.4s ease;
}

.analitics-trans-enter-from,
.analitics-trans-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}

.modal-window__modal-header-text {
  font-weight: 500;
  font-size: 18px;
}

.modal-window__modal-header-close {
  font-size: 20px;
  cursor: pointer;
  opacity: 0.5;
  transition: all 0.3s;
  font-weight: 600;
}

.modal-window__modal-header-close:hover {
  opacity: 1;
}

.modal-window__modal-body {
  padding: 6px 15px 6px 15px;
  font-size: 13px;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>