<template>
  <div>
    <header class="menu">
      <ul>
        <li>
          <router-link to="/">
            <img class="logo-img" src="../../assets/logo.png" />
          </router-link>
        </li>

        <li class="authentication" v-if="!auth.isLoggedIn()">
          <router-link to="/registration/" class="btn btn-menu">Registration</router-link>
        </li>
        <li class="authentication" v-if="!auth.isLoggedIn()">
          <router-link to="/login/" class="btn btn-menu">Login</router-link>
        </li>

        <li class="authentication" v-if="auth.isLoggedIn()">
          <a v-on:click="logout()" class="btn btn-menu">Logout</a>
        </li>

        <li class="authentication" v-if="auth.isLoggedIn()">
          <a routerLink class="user-avatar">
            <div class="circle">{{auth.getEmail().charAt(0).toUpperCase()}}</div>
          </a>
        </li>
      </ul>
      <div class="clearfix"></div>
    </header>
  </div>
</template>

<script>
import AuthService from "../Core/Auth";

export default {

  methods: {
    logout() {
      AuthService.logout();
    }
  },

  data(){
    return{
      auth: AuthService
    }
  }
};
</script>

<style scoped>
.menu {
  padding: 0 10%;
  height: 110px;
  background-color: #313131;
  -moz-window-shadow: initial;
}

ul {
  list-style-type: none;
  margin: 0px;
}

li {
  float: left;
  vertical-align: middle;
  line-height: 110px;
}

.authentication {
  float: right;
}

.logo-img {
  height: 80px;
  width: 80px;
}

.logo-img:hover,
.avatar:hover,
.circle:hover {
  opacity: 80%;
}

.btn-menu {
  font-size: 120%;
  font-weight: bolder;
  color: white;
}

.btn-menu:hover {
  color: rgb(252, 251, 184);
}

.btn-menu:focus {
  color: rgb(238, 235, 85);
}

.btn-menu:active {
  background: none;
}

.avatar,
.circle {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  object-fit: cover;
}

.circle {
  margin-top: 60%;
  line-height: 50px;
  background: #5b87ff;
  position: relative;
  text-align: center;
}

.user-avatar {
  text-decoration: none;
  color: rgb(252, 251, 184);
  font-weight: bold;
  font-size: 200%;
}

.btn {
  display: inline-block;
  padding: 6px 12px;
  margin-bottom: 0;
  font-size: 14px;
  font-weight: bold;
  line-height: 1.42857143;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  cursor: pointer;
  border: none;
}
</style>
