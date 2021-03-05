<template>
  <div class="profilePage container-fluid ">
    <div class="row mt-3">
      <div class="col-3 text-center mt-2" v-if="state.profile">
        <img id="profile" :src="state.profile.picture" alt="">
        <p> {{ state.profile.name }}</p>
        <!-- TODO Add the Vaults.length and Keeps.length -->
      </div>
    </div>
    <div class="row">
      <h4>Vaults:</h4>
      <Vault v-for="v in state.vaults" :key="v.id" :vault-props="v" />
    </div>
    <div class="row">
      <h4>Keeps:</h4>
      <Keep v-for="k in state.keeps" :key="k.id" :keep-props="k" />
    </div>
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
export default {
  name: 'ProfilePage',
  setup() {
    // const router = useRouter()
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      selectedVault: computed(() => AppState.activeVault)
    })
    onMounted(async() => {
      try {
        await profilesService.getById(route.params.id)
        await keepsService.getByProfileId(route.params.id)
        await vaultsService.getByProfileId(route.params.id)
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      state

    }
  }
}
</script>

<style scoped>

#profile{
  border-radius: 50%;
  max-height: 70px;
}
</style>
