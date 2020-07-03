<template>
  <div>
    <button v-on:click="addItemClick()" v-if="auth.isAdmin()">Add new item</button>
    <p v-else><br></p>

    <Dialog ref="dialog">
      <component :is="itemForm" v-on:create="onCreate($event)"></component>
    </Dialog>

    <ItemsList ref="itemsList" v-if="auth.isLoggedIn()"/>
    <p v-else class="centerText">Login to view content</p>
  </div>
</template>

<script>
import ItemsList from "./ItemsList.vue";
import Dialog from "./Common/Dialog.vue";
import AuthService from "./Core/Auth";
import ItemForm from "./ItemForm.vue";

export default {
  components: {
    ItemsList,
    Dialog,
    ItemForm
  },

  methods: {
    addItemClick() {
      this.$refs.dialog.open();
    },

    onCreate(item) {
      this.$refs.itemsList.addItem(item);
      this.$refs.dialog.close();
    }
  },

  data() {
    return {
      items: [],
      itemForm: "ItemForm",
      auth: AuthService
    };
  },

};
</script>

<style scoped>

button {
  background-color: rgb(201, 248, 201);
  height: 50px;
  width: 150px;
  border: 1px solid rgb(12, 82, 12);
  font-size: 120%;
  margin: 20px 10%;
  display: block;
}

.centerText{
  text-align: center;
  width: 100%;
}

</style>
