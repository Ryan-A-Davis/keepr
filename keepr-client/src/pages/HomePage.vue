<template>
  <div class="container-fluid">
    <div class="row justify-content-around">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep-props="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
// import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
  name: 'Home',
  setup() {
    // const router = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      newKeep: {},
      keep: computed(() => AppState.activeKeep)
    })
    onMounted(async() => {
      try {
        await keepsService.getAll()
        await vaultKeepsService.getAll()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      state,
      async create() {
        try {
          await keepsService.create(state.newKeep)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
</style>
