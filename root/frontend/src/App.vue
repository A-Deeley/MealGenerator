<script lang="ts" setup>
import { useUserStore } from './stores/userStore'

const userStore = useUserStore()
</script>

<template>
  <nav class="navbar">
    <RouterLink to="/">Home</RouterLink>
    <div class="navbar" v-if="userStore.isAuthenticated">
      <RouterLink to="/households">Households</RouterLink>
      <RouterLink to="/tags">Tags</RouterLink>
    </div>
  </nav>
  <RouterView />
</template>

<style lang="css" scoped>
.navbar {
  display: flex;
  gap: 1rem;
}

.navbar * {
  transition: 50ms ease-in-out;
}

.navbar a:hover {
  transform: translateY(-2px);
  text-decoration: underline;
}
</style>
