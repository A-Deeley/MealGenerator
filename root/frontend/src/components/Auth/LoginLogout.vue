<script lang="ts" setup>
import { useUserStore } from '@/stores/userStore'
import { storeToRefs } from 'pinia'
import { ref } from 'vue'

const userStore = useUserStore()
const { isAuthenticated, loading, error } = storeToRefs(userStore)
const email = ref('')
const password = ref('')

async function login() {
  await userStore.login(email.value, password.value)
  email.value = ''
  password.value = ''
}

async function loginAndrew() {
  await userStore.login('andrew@test.com', 'aaa123')
}

async function loginJean() {
  await userStore.login('jean@test.com', 'info2020')
}

async function loginLydia() {
  await userStore.login('lydia@test.com', 'biscuit')
}
</script>

<template>
  <div v-if="isAuthenticated">
    <button @click="userStore.logout">Logout</button>
  </div>
  <div v-else id="login">
    <h2 id="header">Login</h2>
    <label>Email</label>
    <input type="text" placeholder="Email" v-model="email" :disabled="loading" />
    <label>Password</label>
    <input
      type="password"
      placeholder="Password"
      v-model="password"
      :disabled="userStore.loading"
    />
    <button v-if="!loading" @click="login">Login</button>
    <span v-else>Logging in...</span>
    <span class="error-msg" v-if="error">{{ userStore.error }}</span>
    <button @click="loginAndrew">Login andrew</button>
    <button @click="loginJean">Login jean</button>
    <button @click="loginLydia">Login lydia</button>
  </div>
</template>

<style lang="css" scoped>
#login {
  display: grid;
  grid-template-columns: auto 1fr;
  gap: 1rem;
  width: fit-content;
}

#header {
  grid-column: 1 / span 2;
}

.error-msg {
  color: red;
  font-weight: bold;
}
</style>
