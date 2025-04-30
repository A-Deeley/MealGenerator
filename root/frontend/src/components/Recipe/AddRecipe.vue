<script lang="ts" setup>
import { useHouseholdStore } from '@/stores/householdStore'
import { useRecipeStore } from '@/stores/recipeStore'
import { type RecipeTagResponse } from '@/types/backend.interface'
import { storeToRefs } from 'pinia'
import { computed, ref, useTemplateRef } from 'vue'
import RecipeTagItem from '../RecipeTags/RecipeTagItem.vue'
const props = defineProps<{
  id: string
  onAddClicked?: () => void
  onCancelClicked: () => void
}>()
const selectRef = useTemplateRef('selectRef')

const householdStore = useHouseholdStore()
householdStore.getHousehold(props.id)

const recipeStore = useRecipeStore()
recipeStore.getTags()
const { tags } = storeToRefs(recipeStore)
const availableTags = computed(() => {
  return tags.value.filter((t) => !recipeTags.value.find((e) => e.id == t.id))
})

const recipeTitle = ref('')
const recipeTags = ref<RecipeTagResponse[]>([])

async function onRecipeAdded() {
  await householdStore.addRecipe(+props.id, {
    title: recipeTitle.value,
    tags: recipeTags.value.map((t) => t.id),
  })
  recipeTitle.value = ''
  recipeTags.value = []
  if (props.onAddClicked) props.onAddClicked()
}

const onChange = ({ target }: Event) => {
  const selectElement = target as HTMLSelectElement
  const selectedId = +selectElement.value
  const item = tags.value.find((e) => e.id == selectedId)
  if (!item) {
    selectRef.value!.value = ''
    return
  }

  recipeTags.value.push({ ...item })
  selectRef.value!.value = ''
}

function onRecipeCanceled() {
  recipeTitle.value = ''
  recipeTags.value = []
  props.onCancelClicked()
}
</script>

<template>
  <div v-if="!householdStore.householdId">Loading...</div>
  <div v-else>
    <h2>Add a recipe to {{ householdStore.householdId.name }}</h2>
    <input type="text" v-model="recipeTitle" placeholder="Enter title..." />
    <select @change="onChange" ref="selectRef">
      <option value="">Choose one or more tags</option>
      <option v-for="tag in availableTags" :key="tag.id" :value="tag.id">{{ tag.name }}</option>
    </select>
    <span v-if="recipeTags.length == 0" class="warning"
      >A recipe without tags will not be included in the algorithm. Select at least one tag.</span
    >
    <RecipeTagItem v-for="tag in recipeTags" :key="tag.id" :tag="tag" />
    <div class="recipe-btns-container">
      <button @click="onRecipeAdded">Add</button>
      <button @click="onRecipeCanceled">Cancel</button>
    </div>
  </div>
</template>
<style lang="css" scoped>
.recipe-btns-container {
  display: flex;
  gap: 2rem;
}

.warning {
  color: orange;
}
</style>
