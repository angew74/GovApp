<template>
    <div role="tablist" style="max-width: 80rem;margin-left:360px" v-if="Object.entries(sezionemodel).length !== 1">
        <b-card-group v-if="sezionemodel.statusSezione" deck>
            <b-card no-body class="mb-1 border-0">
                <b-card-header header-tag="header" class="p-1 border-0" role="tab">
                    <b-button block v-b-toggle.sezionestatus variant="dark">Status sezione numero {{sezionemodel.numeroSezione}}</b-button>
                </b-card-header>
                <b-collapse id="sezionestatus" visible accordion="my-accordion" role="tabpanel">
                    <b-card-body>
                        <b-card-text>
                            <div v-for="s in sezionemodel.statusSezione" style="font-size:0.90em;font-weight:bolder" :key="s.id">
                                <b-list-group horizontal>
                                    <b-list-group-item style="border-right:none">
                                        <p-check class="p-icon p-round p-jelly"
                                                 color="success" off-color="danger" toggle
                                                 id="pervenuto"
                                                 v-model="s.pervenuto"
                                                 name="pervenuto"                                                
                                                 :disabled="true">
                                            <label slot="off-label"></label>
                                        </p-check>
                                    </b-list-group-item>
                                    <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                                        <label>{{s.nome}}</label>
                                    </b-list-group-item>                                   
                                </b-list-group>
                            </div>
                        </b-card-text>
                    </b-card-body>
                </b-collapse>
            </b-card>
        </b-card-group>
    </div>
</template>

<script>
    export default {
        name: 'statussezioneaw',
        props: ['sezionemodel'],
        mounted() {
        }
    };

</script>
