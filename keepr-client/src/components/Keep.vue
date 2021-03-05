<template>
  <div class="keep col-4-lg-3 card" type="button" :data-target="'#modal' + keepProps.id " data-toggle="modal" :style="`background-image: url(${keepProps.img})`">
    <div class="row align-self-end justify-content-between mb-1">
      <div class="col-4">
        <p>{{ keepProps.name }}</p>
      </div>
      <div class="col-4">
        <img class="rounded" :src="keepProps.creator.picture" alt="">
      </div>
      <KeepModal :keep-props="keepProps" />
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
export default {
  name: 'Keep',
  props: {
    keepProps: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      account: computed(() => AppState.account),
      targetKeep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      async delete() {
        try {
          await keepsService.delete(props.p.id)
        } catch (error) {
          logger.error(error)
        }
      }
      //   setActive(id) {
      //     targetKeep =
      //   }
    }
  }
}
</script>

<style lang="scss" scoped>

.keep{
  min-height: 150px;
  background-position:center;
  background-repeat: no-repeat;
  background-size: cover;
}
.img{
  max-height: 15px;
}

</style>
