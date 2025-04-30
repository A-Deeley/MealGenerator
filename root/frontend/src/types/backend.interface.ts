export interface SignUpPostRequest {
  email: string
  password: string
  nickname?: string
}

export interface PostRecipeRequest {
  title: string
  tags: number[]
}

export interface PutRecipeRequest {
  id: number
  title: string
}

export interface RecipeResponse {
  id: number
  title: string
  tags: RecipeTagResponse[]
}

export interface HouseholdResponse {
  id: number
  name: string
}

export interface HouseholdListResponse {
  ownerOf: HouseholdResponse[]
  memberOf: HouseholdResponse[]
}

export interface HouseholdViewResponse {
  id: number
  name: string
  recipes: RecipeResponse[]
  pendingInvites: HouseholdUserInvite[]
  ownerId: string
  isOwner: boolean
  members: MemberResponse[]
}

export interface HouseholdUserInvite {
  id: number
  household: HouseholdListResponse
  user: MemberResponse
}

export interface PostHouseholdRequest {
  name: string
}

export interface MemberResponse {
  id: string
  nickname: string
}

export interface UserDetailsResponse {
  id: string
  nickname: string
  pendingInvites: HouseholdUserInvite[]
}

export interface ChangeInviteRequest {
  inviteId: number
  accepted: boolean
}

export interface RecipeTagResponse {
  id: number
  name: string
  colour: string
}

export interface PostRecipeTagRequest {
  id?: number
  name: string
  colour: string
}

export interface GetRecipeGenOptions {
  id?: number
  householdId: number
  recipe: RecipeResponse
  maxWeeklyOccurence: number
  minReoccurenceDelayWeeks: number
  minWeeklyOccurence: number
}

export interface PostRecipeGenOption {
  householdId: number
  recipeId: number
  maxWeeklyOccurence: number
  minReoccurenceDelayWeeks: number
  minWeeklyOccurence: number
}
