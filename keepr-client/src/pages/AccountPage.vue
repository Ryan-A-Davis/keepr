<template>
  <div class="accountPage container-fluid ">
    <div class="row mt-3 justify-content-around">
      <div class="col-3 flex-nowrap">
        <h4>
          Vaults: {{ state.vaults.length }} <button type="button" class="btn btn-primary mx-3" data-toggle="modal" data-target="#vaultCreateModal">
            + New Vault
          </button>
        </h4>
      </div>
      <div class="col-3 flex-nowrap">
        <h4>
          Keeps: {{ state.keeps.length }}  <button type="button" class="btn btn-primary mx-3" data-toggle="modal" data-target="#keepCreateModal">
            + New Keep
          </button>
        </h4>
      </div>
      <div class="col-3 text-right mt-2" v-if="state.profile">
        <img id="profile" :src="state.profile.picture" alt="">
      </div>
    </div>
    <div class="row justify-content-around text-center my-5">
      <h4>{{ state.profile.name }}'s Vaults</h4>
    </div>
    <div class="row">
      <Vault v-for="v in state.vaults" :key="v.id" :vault-props="v" />
    </div>
    <div class="row justify-content-around text-center my-5">
      <h4>{{ state.profile.name }}'s Keeps</h4>
    </div>
    <div class="grid-container">
      <Keep v-for="k in state.keeps" :key="k.id" :keep-props="k" :class="'item'" />
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

.grid-container {
  width: 100%;
  margin: 20px auto;
  columns: 4;
  column-gap: 20px;

}

.item {
  display: block;
  margin: 0, 0, 15px;
  width: 100%;
  padding: 10px;
  overflow: hidden;
  break-inside: avoid;
  box-sizing: border-box;
  border: solid 2px black;
  box-shadow: 5px 5px 5px rgba(0, 0, 0, 0.5);
  border-radius: 5px;
}

#profile{
  border-radius: 50%;
  max-height: 50px;
}
</style>
