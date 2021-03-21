<template>
  <div class="component container-fluid">
    <div class="row justify-content-between mt-3">
      <div class="col-4 text-center mx-5 mt-2">
        <h1>{{ state.vault.name }}</h1>
        <p class="desc">
          {{ state.vault.description }}
        </p>
      </div>
      <div class="col-3 justify-content-end">
        <button class="btn btn-danger" @click="removeVault()">
          Delete Vault
        </button>
      </div>
    </div>
    <div class="row justify-content-around">
      <h2>Keeps</h2>
      <div v-for="keep in state.keeps" :key="keep.id">
        <Keep :keep-props="keep" />
        <!-- TODO delete relationship vk from here -->
        <button v-if="keep.creatorId === state.user.id" class="btn btn-danger" @click="removeKeep(keep.vaultKeepId)">
          Delete
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { computed, onMounted, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import NotificationService from '../services/NotificationsService'
import { vaultKeepsService } from '../services/VaultKeepsService'

export default {
  name: 'VaultDetails',
  setup() {
    const route = useRoute()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault)
    })
    onMounted(async() => {
      try {
        await keepsService.getByVaultId(route.params.id)
        await vaultsService.getById(route.params.id)
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      state,
      route,
      async removeKeep(id) {
        try {
          if (await NotificationService.confirm()) {
            await vaultKeepsService.delete(id)
          } else {
            alert('Changes not saved')
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async removeVault() {
        try {
          if (await NotificationService.confirm()) {
            await vaultsService.delete(route.params.id)
          } else {
            alert('Changes not saved')
          }
        } catch (error) {
          logger.error(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.desc{
  font-size: 150%;
  font-weight: 600;
}
</style>
