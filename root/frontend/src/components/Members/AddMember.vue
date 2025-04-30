<script lang="ts" setup>
import { useHouseholdStore } from '@/stores/householdStore'
import { useUserStore } from '@/stores/userStore'
import type { MemberResponse } from '@/types/backend.interface'
import { computed, ref, useTemplateRef, watch } from 'vue'

const householdStore = useHouseholdStore()
const userStore = useUserStore()
const input = ref('')
const results = ref<MemberResponse[]>()
const showResults = computed(() => !!results.value)
const inputRef = useTemplateRef('inputRef')

watch(input, async (newInput) => {
  if (newInput.length == 0) {
    results.value = undefined
    return
  }
  if (newInput.length < 2) return
  const response = await userStore.find(newInput)
  results.value = response.filter(
    (e) => !householdStore.householdId?.pendingInvites.find((i) => i.user.id === e.id),
  )
  inputRef.value?.focus()
})

async function addMember(member: MemberResponse) {
  await householdStore.inviteMember(member)
  input.value = ''
  results.value = undefined
}
</script>

<template>
  <div id="parent">
    <span class="search-body">
      <input
        ref="inputRef"
        :disabled="userStore.loading"
        type="text"
        class="search"
        v-model="input"
        placeholder="Search"
      />
      <svg
        :class="{ icon: true, 'show-results': showResults }"
        xmlns="http://www.w3.org/2000/svg"
        width="12"
        height="10"
      >
        <polygon points="6,10 0,0 12,0" />
      </svg>
    </span>
    <div class="results" v-if="showResults">
      <span v-if="results?.length == 0">No results found</span>
      <span
        v-else
        class="result-item"
        v-for="result in results"
        :key="result.id"
        @click="addMember(result)"
        >{{ result.nickname }}</span
      >
    </div>
  </div>
</template>

<style lang="css" scoped>
.result-item:hover {
  font-weight: bold;
  border-bottom: 1px solid black;
}

.result-item {
  cursor: pointer;
  border-bottom: 1px solid transparent;
}

.results {
  padding: 0.5rem;
  border-left: 2px solid black;
  border-right: 2px solid black;
  border-bottom: 2px solid black;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  max-height: 10rem;
  overflow-y: scroll;
}

.search-body {
  outline: 1px solid black;
  border-radius: 5px;
  display: flex;
  align-items: center;
  gap: 2px;
  padding-inline: 0.5rem;
}

#parent {
  width: fit-content;
}

.search-body:has(.search:focus) {
  outline: 2px solid lightskyblue;
}

.search {
  border: 0;
  height: 2rem;
}

.search:focus {
  outline: 0;
}

.icon.show-results {
  transform: rotate(0.5turn);
}

.icon {
  transition: 150ms ease-in-out transform;
}
</style>
