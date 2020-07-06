<template>
  <div class="form">
    <h1>Login</h1>
    <form @submit.prevent="onSubmit()">
      <div class="control">
         (email: 1, pass: 1 - admin ///// email: 2, pass: 2 - user)
        <span class="title requiredTitle">Email:)</span>
        <input type="text" id="email" name="email" class="input" v-model="form.email" required />
      </div>

      <div class="control">
        <span class="title requiredTitle">Password:</span>
        <input
          type="password"
          id="password"
          name="password"
          class="input"
          v-model="form.password"
          required
        />
      </div>

      <span class="title" style="color:red" v-if="error">{{error}}</span>

      <div class="control">
        <button type="submit" class="btn_sub">Login</button>
      </div>
    </form>
  </div>
</template>

<script>
import AuthService from "../Core/Auth";
import router from "../../router";

export default {
  data() {
    return {
      form: {
        email: "",
        password: ""
      },
      error: null
    };
  },
  methods: {
    onSubmit() {
      let creds = this.form;

      AuthService.login(creds)
        .then(resp => {
          if (resp) {
            this.error = null;
            router.push("/");
            router.go();
          }
        })
        .catch(() => {
          this.error = "Wrong email or password";
        });
    }
  }
};
</script>

<style scoped>
.form{
  margin: 50px;
}
</style>