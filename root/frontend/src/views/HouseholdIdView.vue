<script lang="ts" setup>
import AddMember from '@/components/Members/AddMember.vue'
import MemberItem from '@/components/Members/MemberItem.vue'
import PendingMember from '@/components/Members/PendingMember.vue'
import AddRecipe from '@/components/Recipe/AddRecipe.vue'
import RecipeItem from '@/components/Recipe/RecipeItem.vue'
import { useHouseholdStore } from '@/stores/householdStore'
import { computed, ref } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const id = route.params.id[0]

const householdStore = useHouseholdStore()
householdStore.getHousehold(id)

const addRecipe = ref(false)
const confirmDelete = ref(false)

const isOwner = computed(() => householdStore.householdId?.isOwner)

async function deleteHousehold() {
  if (!confirmDelete.value) return
  await householdStore.deleteHousehold(id)
  confirmDelete.value = false
}
</script>

<template>
  <div v-if="!householdStore.householdId">Loading...</div>
  <div v-else class="content">
    {{ householdStore.householdId.name }}
    <RouterLink v-if="isOwner" :to="`/households/${id}/recipegeneration`"
      >Configure recipe generation</RouterLink
    >
    <div class="delete-confirm">
      <button v-if="isOwner" class="btn" @click="confirmDelete = true">Delete household</button>
      <button class="btn" v-if="confirmDelete" @click="deleteHousehold">Confirm</button>
    </div>
    <h2>Recipes</h2>
    <div v-if="householdStore.householdId?.recipes" class="recipes-container">
      <RecipeItem
        v-for="recipe in householdStore.householdId.recipes"
        :recipe="recipe"
        :key="recipe.id"
      />
    </div>
    <button v-if="!addRecipe" @click="addRecipe = true" class="btn">Add recipe</button>
    <div v-if="addRecipe">
      <AddRecipe :id="id" @cancel-clicked="addRecipe = false" @add-clicked="addRecipe = false" />
    </div>

    <div>
      <h3>Members</h3>
      <MemberItem
        v-for="member in householdStore.householdId.members"
        :key="member.id"
        :member="member"
      />
    </div>
    <div v-if="isOwner">
      <h2>Invite someone</h2>
      <AddMember class="add-member" />
      <h3>Pending invites</h3>
      <div class="recipes-container">
        <PendingMember
          v-for="member in householdStore.householdId.pendingInvites"
          :key="member.user.id"
          :member="member"
        />
      </div>
    </div>
  </div>
</template>

<style lang="css" scoped>
.add-member {
  margin-left: 1rem;
}

.content {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  width: fit-content;
}

.btn {
  width: 8rem;
}

.delete-confirm {
  display: flex;
  gap: 2rem;
}

.recipes-container {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>
