
<template>
    <div role="tablist" style="max-width: 80rem;margin-left:360px" v-if="Object.entries(sezionemodel).length !== 1">
        <b-card no-body class="mb-1 border-0">
            <b-card-header header-tag="header" class="p-1 border-0" role="tab">
                <b-button block v-b-toggle.sezione variant="dark">Sezione numero {{sezionemodel.numeroSezione}}</b-button>
            </b-card-header>
            <b-collapse id="sezione" visible accordion="my-accordion" role="tabpanel">
                <b-card-body>
                    <b-card-text>
                        <b-list-group horizontal class="list-group-flush">
                            <b-list-group-item>
                                Numero cabina  <b-badge variant="secondary" pill>{{sezionemodel.cabina}}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                Municipio <b-badge variant="secondary" pill> {{sezionemodel.municipio}}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                              Tipo Elezione  <b-badge variant="secondary" pill>  {{sezionemodel.descrizioneElezione}}</b-badge>
                            </b-list-group-item>
                        </b-list-group>
                    </b-card-text>
                </b-card-body>
            </b-collapse>
        </b-card>

        <b-card no-body class="mb-1 border-0">
            <b-card-header header-tag="header" class="p-1 border-0" role="tab">
                <b-button block v-b-toggle.plesso variant="dark">Plesso</b-button>
            </b-card-header>
            <b-collapse id="plesso" accordion="my-accordion" role="tabpanel">
                <b-card-body>
                    <b-card-text>
                        <b-list-group horizontal class="list-group-flush">
                            <b-list-group-item>
                                <v-icon name="person-booth" />
                                Tipo Sezione    <b-badge variant="secondary" pill> {{sezionemodel.descrizioneSezione}}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <v-icon name="person-booth" />
                                Struttura <b-badge variant="secondary" pill> {{sezionemodel.descrizionePlesso}}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <v-icon name="person-booth" />
                                Ubicazione <b-badge variant="secondary" pill>{{sezionemodel.ubicazionePlesso}}</b-badge>
                            </b-list-group-item>
                        </b-list-group>
                    </b-card-text>
                </b-card-body>
            </b-collapse>
        </b-card>

        <b-card no-body class="mb-1 border-0">
            <b-card-header header-tag="header" class="p-1 border-0" role="tab">
                <b-button block v-b-toggle.iscritti variant="dark">Iscritti</b-button>
            </b-card-header>
            <b-collapse id="iscritti" accordion="my-accordion" role="tabpanel">
                <b-card-body>
                    <b-card-text>
                        <b-list-group horizontal class="list-group-flush">
                            <b-list-group-item class="align-items-center">
                                <v-icon name="venus" />
                                Iscritti Femmine  <b-badge variant="secondary" pill> {{sezionemodel.iscritti.iscrittifemmine}}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <v-icon name="mars" />
                                Iscritti Maschi  <b-badge variant="secondary" pill> {{sezionemodel.iscritti.iscrittimaschi}}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <v-icon name="venus-mars" />
                                Iscritti Totali  <b-badge variant="secondary" pill> {{sezionemodel.iscritti.iscrittitotali}}</b-badge>
                            </b-list-group-item>
                        </b-list-group>
                    </b-card-text>
                </b-card-body>
            </b-collapse>
        </b-card>
    </div>
</template>



<script>
    export default {
        name: 'sezioneaw',
        props: ['sezionemodel']
    };
</script>