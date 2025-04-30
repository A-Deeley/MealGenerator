<script lang="ts" setup>
import LoginLogout from '@/components/Auth/LoginLogout.vue'
import { useUserStore } from '@/stores/userStore'
import { storeToRefs } from 'pinia'

const userStore = useUserStore()
userStore.loadUser()
const { isAuthenticated, user } = storeToRefs(userStore)
</script>

<template>
  <div id="page">
    <p>
      Welcome <span v-if="isAuthenticated">{{ user?.nickname }}</span
      >!
    </p>
    <LoginLogout />
    <p v-if="!isAuthenticated">New user? Register <RouterLink to="/register">here</RouterLink>!</p>
    <p v-else>
      <span v-if="user && user.pendingInvites?.length > 0"
        >You have {{ user.pendingInvites.length }} pending invites. Click
        <RouterLink to="user/invites">here</RouterLink> to manage.</span
      >
    </p>
  </div>
</template>

<style lang="css" scoped></style>
