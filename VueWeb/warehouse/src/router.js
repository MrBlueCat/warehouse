import VueRouter from 'vue-router';

import Login from './components/Authorization/Login.vue'
import Registration from './components/Authorization/Registration.vue'
import Items from './components/Items.vue'

let routes = [
  {
    path: '/',
    component: Items,
  },
  {
    path: '/login',
    component: Login,
  },
  {
    path: '/registration',
    component: Registration
  }
];

let router = new VueRouter({
  routes
});

export default router;