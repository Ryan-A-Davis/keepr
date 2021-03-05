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
      <h4>Vaults:</h4>
      <Vault v-for="v in state.vaults" :key="v" :vault-props="v" />
    </div>
    <div class="row">
      <h4>Keeps:</h4>
      <Keep v-for="k in state.keeps" :key="k" :keep-props="k" />
    </div>
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { useRouter } from 'vue-router'
import { accountService } from '../services/AccountService'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
export default {
  name: 'AccountPage',
  setup() {
    const router = useRouter()
    // const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      selectedVault: computed(() => AppState.activeVault)
    })
    onMounted(async() => {
      try {
        await accountService.getVaults()
        await accountService.getKeeps()
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
