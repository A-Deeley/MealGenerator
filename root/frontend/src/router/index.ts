import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import RegisterView from '@/views/RegisterView.vue'
import HouseholdsView from '@/views/HouseholdsView.vue'
import HouseholdIdView from '@/views/HouseholdIdView.vue'
import InvitesView from '@/views/InvitesView.vue'
import TagsView from '@/views/TagsView.vue'
import ConfigureRecipeOptionsView from '@/views/ConfigureRecipeOptionsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
    },
    {
      path: '/households',
      name: 'households',
      component: HouseholdsView,
    },
    {
      path: '/households/:id',
      name: 'householdId',
      component: HouseholdIdView,
      props: true,
    },
    {
      path: '/households/:id/recipegeneration',
      name: 'householdRecipeGenOpts',
      component: ConfigureRecipeOptionsView,
    },
    {
      path: '/user/invites',
      name: 'userInvites',
      component: InvitesView,
    },
    {
      path: '/tags',
      name: 'tags',
      component: TagsView,
    },
  ],
})

const anonymousRoutes = ['home', 'register']

router.beforeEach(async (to) => {
  const routeName = to.name as string
  if (anonymousRoutes.indexOf(routeName) > -1) return
  else
    try {
      await fetch('https://localhost:7163/Access', {
        method: 'get',
        credentials: 'include',
      })
      sessionStorage.setItem('auth', 'true')
      return
    } catch {
      sessionStorage.clear()
      return { name: 'home' }
    }
})

export default router
