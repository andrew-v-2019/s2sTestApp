<template>
  <button
    :disabled="disabled || loading"
    class="button button_green"
    v-bind:style="{ 'background-color': color }"
  >
    <span v-if="!loading">{{ text }}</span>
    <loader :loading="loading" />
  </button>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import Loader from "./Loader.vue";

export default defineComponent({
  name: "LoaderButton",
  components: { Loader },
  props: {
    loading: {
      type: Boolean,
      default: false,
    },

    color: {
      type: String,
      default: `#4caf50`,
    },

    text: String,
    disabled: {
      type: Boolean,
      default: false,
    },
  },
});
</script>

<style scoped>
.button {
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #4caf50;
  border: none;
  color: black;
  padding: 8px 16px;
  text-align: center;
  text-decoration: none;
  font-size: 16px;
  transition: all 0.4s;
  cursor: pointer;
  border: 2px solid #4caf50;
  border-radius: 5px;
  opacity: 0.7;
}

.button__text_loading {
  margin-right: 5px;
}

.button:hover {
  background-color: #008cba;
  color: white;
  opacity: 1;
}
</style>>