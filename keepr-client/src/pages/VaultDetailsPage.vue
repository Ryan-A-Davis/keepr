<template>
  <div class="component">
    <div class="row mt-3">
      <div class="col-4 text-center mx-5 mt-2">
        <h1>{{ state.vault.name }}</h1>
      </div>
      <div class="col-3">
        <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#staticBackdrop' + route.params.id">
        </button>
      </div>
    </div>
    <div class="row justify-content-around">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep-props="keep" />
    </div>
  </div>
</template>m

<script>
import { AppState } from '../AppState'
import { computed, onMounted, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'

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
    return { state }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
