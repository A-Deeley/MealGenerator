<script lang="ts" setup>
import { ref } from 'vue'
import type { SignUpPostRequest } from '@/types/backend.interface'
import { useUserStore } from '@/stores/userStore'

const userStore = useUserStore()
const email = ref('')
const password = ref('')
const nickname = ref('')

const resetForm = () => {
  email.value = ''
  password.value = ''
  nickname.value = ''
}

const createSignUpArgs = () => {
  const requestArgs: SignUpPostRequest = {
    email: email.value,
    password: password.value,
    nickname: nickname.value.length > 0 ? nickname.value : undefined,
  }
  return requestArgs
}

const onRegisterClicked = async () => {
  const args = createSignUpArgs()
  userStore.createUser(args)
  resetForm()
}
</script>

<template>
  <div id="page">
    <h2>Register</h2>
    <input type="text" placeholder="Nickname" v-model="nickname" />
    <input type="text" placeholder="Email" v-model="email" />
    <input type="password" placeholder="Password" v-model="password" />
    <button @click="onRegisterClicked">Register</button>
  </div>
</template>

<style lang="css" scoped>
#page {
  margin: 0 auto;
  width: 25%;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>
