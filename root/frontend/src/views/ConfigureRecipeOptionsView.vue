<script setup lang="ts">
import { useHouseholdStore } from '@/stores/householdStore'
import { useRecipeOptionsStore } from '@/stores/recipeOptionsStore'
import { type PostRecipeGenOption } from '@/types/backend.interface'
import { storeToRefs } from 'pinia'
import { ref } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const id = route.params.id[0]

const optionsStore = useRecipeOptionsStore()
const householdStore = useHouseholdStore()
householdStore.getHousehold(id)
optionsStore.GetOptions(+id)
const { householdId } = storeToRefs(householdStore)
const { options } = storeToRefs(optionsStore)
const selectedRecipeId = ref<number>(-1)
const defaultOpts = {
  minReoccurenceDelayWeeks: 0,
  minWeeklyOccurence: 0,
  householdId: +id,
  maxWeeklyOccurence: 0,
  recipeId: 0,
}
const recipeConfig = ref<PostRecipeGenOption>({ ...defaultOpts })

async function AddRecipeConfig() {
  recipeConfig.value.recipeId = selectedRecipeId.value
  await optionsStore.PostOptions(recipeConfig.value)

  recipeConfig.value = { ...defaultOpts }
  selectedRecipeId.value = -1
}
</script>

<template>
  <RouterLink :to="`/households/${id}`">Back to household</RouterLink>
  <h2>Configure recipes</h2>
  <div class="configure-new-recipe">
    <h3>New configuration</h3>
    <select v-model="selectedRecipeId">
      <option :value="-1">Select a recipe to configure</option>
      <option v-for="recipe in householdId?.recipes" :key="recipe.id" :value="recipe.id">
        {{ recipe.title }}
      </option>
    </select>
    <div class="container" v-if="selectedRecipeId > 0">
      <label for="minReoccurenceDelayWeeks">Minimum reoccurence (weeks)</label>
      <input
        id="minReoccurenceDelayWeeks"
        type="number"
        v-model="recipeConfig.minReoccurenceDelayWeeks"
      />
      <label for="minWeeklyOccurence">Minimum occurences per week</label>
      <input id="minWeeklyOccurence" type="number" v-model="recipeConfig.minWeeklyOccurence" />
      <label for="maxWeeklyOccurence">Maximum occurences per week</label>
      <input id="maxWeeklyOccurence" type="number" v-model="recipeConfig.maxWeeklyOccurence" />
      <button @click="AddRecipeConfig">Save</button>
    </div>
  </div>
  <div v-for="option in options" :key="option.id">
    <h3>{{ option.recipe.title }}</h3>
  </div>
</template>

<style lang="css" scoped>
.configure-new-recipe {
  border: 1px solid black;
  border-radius: 5px;
  padding: 1rem;
  width: fit-content;
}

.container {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  margin-top: 1rem;
}
</style>
