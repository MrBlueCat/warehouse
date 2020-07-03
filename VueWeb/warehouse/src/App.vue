<template>
  <div id="app">
    <Header />
    <router-view></router-view>
  </div>
</template>

<script>
import Header from "./components/Common/Header.vue";

export default {
  name: "App",
  components: {
    Header
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

body {
  margin: 0;
}

.title {
  font-size: 105%;
  font-weight: bold;
  display: block;
  margin: 20px auto 5px;
}

.input {
  width: 70%;
}

button.btn_sub {
  background-color: rgb(201, 248, 201);
  height: 30px;
  width: 100px;
  border: 1px solid rgb(12, 82, 12);
  font-size: 120%;
  margin: 40px 0;
  display: block;
}

.requiredTitle::after {
  content: " *";
  color: red;
}
</style>
