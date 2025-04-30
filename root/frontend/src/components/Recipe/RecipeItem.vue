<script lang="ts" setup>
import { useHouseholdStore } from '@/stores/householdStore'
import type { RecipeResponse } from '@/types/backend.interface'
import RecipeTagItem from '../RecipeTags/RecipeTagItem.vue'

const props = defineProps<{
  recipe: RecipeResponse
}>()

const householdStore = useHouseholdStore()

async function deleteRecipe() {
  await householdStore.removeRecipe(props.recipe.id)
}
</script>

<template>
  <div class="recipe">
    <span
      ><button v-if="householdStore.householdId?.isOwner" @click="deleteRecipe">x</button>
      {{ recipe.title }}</span
    >
    <div class="tags"><RecipeTagItem v-for="tag in recipe.tags" :key="tag.id" :tag="tag" /></div>
  </div>
</template>

<style lang="css" scoped>
.tags {
  display: flex;
  gap: 0.5rem;
}
</style>
