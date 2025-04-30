import router from '@/router'
import type {
  ChangeInviteRequest,
  HouseholdListResponse,
  HouseholdViewResponse,
  MemberResponse,
  PostHouseholdRequest,
  PostRecipeRequest,
} from '@/types/backend.interface'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useHouseholdStore = defineStore('household', () => {
  const userHouseholds = ref<HouseholdListResponse>()
  const householdId = ref<HouseholdViewResponse>()
  const error = ref<string>()

  async function getHouseholds() {
    const response = await fetch('https://localhost:7163/Households', {
      method: 'get',
      credentials: 'include',
    })

    userHouseholds.value = await response.json()
  }

  async function getHousehold(id: string) {
    const response = await fetch(`https://localhost:7163/Households/${id}`, {
      method: 'get',
      credentials: 'include',
    })

    householdId.value = await response.json()
  }

  async function addHousehold(name: string) {
    const request: PostHouseholdRequest = {
      name: name,
    }

    const response = await fetch('https://localhost:7163/Households', {
      method: 'post',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    })

    if (response.status === 201) {
      if (userHouseholds.value) userHouseholds.value.ownerOf.push(await response.json())
    }
  }

  async function deleteHousehold(id: string) {
    try {
      const response = await fetch(`https://localhost:7163/Households/${id}`, {
        method: 'delete',
        credentials: 'include',
      })
      if (response.status === 204) router.push({ name: 'households' })
      else error.value = await response.json()
    } catch (e) {
      error.value = JSON.stringify(e)
    }
  }

  async function addRecipe(id: number, recipe: PostRecipeRequest) {
    const response = await fetch(`https://localhost:7163/Households/${id}/Recipes`, {
      method: 'post',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(recipe),
    })

    if (response.status === 200) {
      await getHousehold(`${id}`)
    }
  }

  async function removeRecipe(recipeId: number) {
    await fetch(`https://localhost:7163/Households/${householdId.value?.id}/Recipes/${recipeId}`, {
      method: 'delete',
      credentials: 'include',
    })

    await getHousehold(`${householdId.value?.id}`)
  }

  async function inviteMember(member: MemberResponse) {
    await fetch(`https://localhost:7163/Households/${householdId.value?.id}/User`, {
      method: 'post',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(member),
    })

    await getHousehold(`${householdId.value?.id}`)
  }

  async function updateInvitation(householdId: number, inviteId: number, accept: boolean) {
    const request: ChangeInviteRequest = {
      inviteId,
      accepted: accept,
    }
    await fetch(`https://localhost:7163/Households/${householdId}/Invite`, {
      method: 'post',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    })
  }

  return {
    userHouseholds,
    householdId,
    getHouseholds,
    addHousehold,
    getHousehold,
    deleteHousehold,
    addRecipe,
    removeRecipe,
    inviteMember,
    updateInvitation,
    error,
  }
})
