<template>
  <div class="form">
    <h1>Registration</h1>
    <form @submit.prevent="onSubmit()">
      <div class="control">
        <span class="title requiredTitle">Email:</span>
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

      <div class="control">
        <span class="title requiredTitle">Confirm password:</span>
        <input
          type="password"
          id="confirmPassword"
          name="confirmPassword"
          class="input"
          v-model="form.confirmPassword"
          required
        />
      </div>

      <span class="title" style="color:red" v-if="error">{{error}}</span>

      <div class="control">
        <button type="submit" class="btn_sub">Register</button>
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
        password: "",
        confirmPassword: ""
      },
      error: null
    };
  },
  methods: {
    onSubmit() {
      let creds = this.form;
      this.error = null;

      if (creds.password !== creds.confirmPassword) {
        this.error = "Your password and confirmation password do not match";
        return;
      }

      AuthService.register(creds)
        .then(resp => {
          if (resp) {
            this.error = null;
            router.push("/");
            router.go();
          }
        })
        .catch(() => {
          this.error = "Smth went wrong. Try again later";
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