<template>
  <div class="dialog" v-if="isShow">
    <button v-on:click="close()" class="closeButt">X</button>

    <div class="content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    close() {
      this.isShow = false;
    },

    open() {
      this.isShow = true;
    }
  },

  data() {
    return {
      isShow: false
    };
  }
};
</script>

<style scoped>
.dialog {
  width: 90%;
  height: 90%;
  border: 2px solid black;
  background-color: rgb(255, 255, 255);
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  margin: auto;
}

.closeButt {
  width: 40px;
  height: 30px;
  font-weight: bold;
  font-size: 150%;
  color: white;
  background-color: rgb(245, 4, 4);
  border: none;
  position: absolute;
  top: 0;
  right: 0;
}

.content {
  margin: 40px auto;
  width: 90%;
  height: 320px;
}
</style>
