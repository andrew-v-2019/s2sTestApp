<template>
  <div class="loading animate" v-if="show"></div>
</template>
<script>
export default {
  props: {
    loading: {
      type: Boolean,
      default: false,
    },
  },

  data: function () {
    return {
      opacity: 0.01,
      show: false,
    };
  },

  created() {
    if (this.loading) {
      this.show = true;
    } else {
      this.show = false;
    }
  },

  watch: {
    loading: function (val) {
      this.show = true;
      if (val) {
        this.show = true;
      } else {
        this.show = false;
      }
    },
  },
};
</script>

<style scoped>
.loading {
  display: inline-flex;
  width: 15px;
  height: 15px;
  background-color: transparent;
  border: 2px solid black;
  border-radius: 100%;
  position: relative;
  justify-content: flex-start;
  align-items: flex-start;
}
.loading::before {
  content: "";
  position: absolute;
  width: 5px;
  height: 5px;
  background-color: white;
  border-radius: 100%;
}

.loading.animate {
  animation-name: animateLoading;
  animation-duration: 0.8s;
  animation-iteration-count: infinite;
  animation-direction: normal;
  animation-timing-function: linear;
}

@keyframes animateLoading {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}
</style>