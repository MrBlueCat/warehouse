<template>
  <div class="itemsList">
    <table>
      <thead>
        <tr>
          <th v-for="name in displayedColumns" v-bind:key="name">{{name}}</th>
          <th style="width: 25px;" v-if="auth.isAdmin()"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" v-bind:key="item.id">
          <td>{{item.name}}</td>
          <td>{{item.manufacturer? item.manufacturer.name : ''}}</td>
          <td>{{item.customer? item.customer.name : ''}}</td>
          <td>{{item.date}}</td>
          <td v-if="auth.isAdmin()">
            <button v-on:click="openEditDialog(item)" class="delButt editButt">E</button>
            <button v-on:click="deleteItem(item)" class="delButt">X</button>
          </td>
        </tr>
      </tbody>
    </table>

    <Dialog ref="dialog">
      <component :is="itemForm" v-bind:itemEdit="itemEdit" v-on:edit="onEdit($event)"></component>
    </Dialog>
  </div>
</template>

<script>
import RequestService from "./Core/RequestService";
import AuthService from "./Core/Auth";
import Dialog from "./Common/Dialog.vue";
import ItemForm from "./ItemForm.vue";

export default {
  components: {
    Dialog,
    ItemForm
  },

  methods: {
    addItem(item) {
      this.items.push(item);
    },

    onEdit(item) {
      let i = this.items.findIndex(e => e.id === item.id);
      this.items.splice(i, 1, item) ;
      this.$refs.dialog.close();
    },

    openEditDialog(item) {
      this.itemEdit = item;
      this.$refs.dialog.open();
    },

    deleteItem(item) {
      RequestService.delete(`items`, item.id).then(() => {
        const index = this.items.indexOf(item);

        if (index > -1) {
          this.items.splice(index, 1);
        }
      });
    }
  },

  data() {
    return {
      displayedColumns: ["Name", "Manufacturer", "Customer", "Date"],
      items: [],
      auth: AuthService,
      itemEdit: null,
      itemForm: "ItemForm"
    };
  },

  created() {
    RequestService.getMany(`items`).then(responce => {
      this.items = responce.data;
    });
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
table,
tr,
td {
  border: 1px solid black;
  border-collapse: collapse;
}

.delButt {
  width: 25px;
  height: 100%;
  font-weight: bold;
  color: white;
  background-color: rgb(245, 4, 4);
  border: none;
}

.editButt {
  background-color: rgb(245, 189, 4);
}

table {
  width: 80%;
  word-wrap: break-word;
  margin: 0 10% 0 10%;
  text-align: left;
}

tr {
  height: 30px;
}

th {
  background-color: #313131;
  color: white;
  font-weight: bold;
  height: 40px;
}
</style>
