<template>
  <div>
    <form name="addForm" @submit.prevent="submit()">
      <span class="title requiredTitle">Item name:</span>
      <input type="text" v-model="item.name" placeholder="Input name" class="input" required />

      <span class="title requiredTitle">Manufacturer:</span>
      <select v-model.number="item.manufacturerId" class="input" required>
        <option
          v-for="manufacturer in manufacturers"
          v-bind:key="manufacturer.id"
          v-bind:value="manufacturer.id"
        >{{manufacturer.name}}</option>
      </select>

      <span class="title requiredTitle">Customer:</span>
      <select v-model.number="item.customerId" class="input" required>
        <option
          v-for="customer in customers"
          v-bind:key="customer.id"
          v-bind:value="customer.id"
        >{{customer.name}}</option>
      </select>

      <button v-if="!itemEdit" type="submit" class="btn_sub">Add</button>
      <button v-if="itemEdit" type="submit" class="btn_sub">Edit</button>
    </form>
  </div>
</template>

<script>
import RequestService from "./Core/RequestService";

export default {
  props: {
    itemEdit: Object
  },

  methods: {
    submit() {
      if (this.itemEdit) {
         RequestService.put("items", this.itemEdit.id, this.item).then(responce => {
          this.$emit("edit", responce.data);
        });
      } else {
        RequestService.post("items", this.item).then(responce => {
          this.$emit("create", responce.data);
        });
      }
    },
  },

  data() {
    return {
      item: {
        name: "",
        manufacturerId: null,
        customerId: null
      },
      manufacturers: [],
      customers: []
    };
  },

  created() {
    RequestService.getMany("manufacturers").then(responce => {
      this.manufacturers = responce.data;
    });

    RequestService.getMany("customers").then(responce => {
      this.customers = responce.data;
    });

    if (this.itemEdit) {
      this.item.name = this.itemEdit.name;
      this.item.manufacturerId = this.itemEdit.manufacturer.id;
      this.item.customerId = this.itemEdit.customer.id;
    }
  }
};
</script>

<style>
</style>
