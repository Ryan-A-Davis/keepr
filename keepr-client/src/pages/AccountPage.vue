<template>
  <div class="accountPage container-fluid ">
    <button>Add Keep</button>
    <div class="row mt-3">
      <div class="col-3 text-center mt-2" v-if="state.profile">
        <img id="profile" :src="state.profile.picture" alt="">
        <p> {{ state.profile.name }}</p>
      </div>
    </div>
    <div class="row">
      <h4>Vaults: {{ state.vaults.length }}</h4>
      <button type="button" class="btn btn-primary mx-5" data-toggle="modal" data-target="#vaultCreateModal">
        + New Vault
      </button>
      <!-- TODO Create vault form -->
      <Vault v-for="v in state.vaults" :key="v.id" :vault-props="v" />
    </div>
    <div class="row">
      <h4>Keeps: {{ state.keeps.length }}</h4>
      <button type="button" class="btn btn-primary mx-5" data-toggle="modal" data-target="#keepCreateModal">
        + New Keep
      </button>
      <!-- TODO Create Keep form -->
      <Keep v-for="k in state.keeps" :key="k.id" :keep-props="k" />
    </div>
    <VaultCreateModal />
    <KeepCreateModal />
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import { profilesService } from '../services/ProfilesService'
export default {
  name: 'AccountPage',
  setup() {
    const router = useRouter()
    const route = useRoute()
    // const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      selectedVault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account)

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
      state,
      async goToVault(id) {
        await vaultsService.getById(id)
        router.push({ name: 'VaultDetails', params: { id: state.selectedVault.id } })
      },

      async createVault() {
        try {
          await vaultsService.create()
        } catch (error) {
          logger.error(error)
        }
      },
      async createKeep() {
        try {
          await keepsService.create()
        } catch (error) {
          logger.error(error)
        }
      }
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
