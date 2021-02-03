<template>    
        <!-- Tabs with card integration -->
        <div style="font-size: 1.2em;text-align: left;min-height: 300px;margin-left: 360px;max-width: 80rem;">
            <b-tabs v-model="tab"                  
                    active-nav-item-class="font-weight-bold text-uppercase text-danger"    
                    nav-item-class="font-weight-bold text-uppercase text-info"
                    content-class="mt-3">
                <b-tab>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Sezione
                    </template>
                    <div class="container" style="font-size:0.8em">
                        <app-research v-bind:form="form" @research="cercasezione"></app-research>
                    </div>
                </b-tab>
                <b-tab>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Municipio
                    </template>
                    <div class="container" style="font-size:0.8em">
                        <app-municipio v-bind:form="form"  @research="cercamunicipio"></app-municipio>                      
                    </div>
                </b-tab>
                <b-tab>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Lista
                    </template>
                    <div class="container" style="font-size:0.8em">
                        <app-lista v-bind:form="form"  @research="cercalista"></app-lista>                      
                    </div>
                </b-tab>
                <b-tab>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Sindaco
                    </template>
                    <div class="container" style="font-size:0.8em">
                        <app-sindaco v-bind:form="form" @research="cercasindaco"></app-sindaco>
                    </div>                   
                </b-tab>
            </b-tabs>      
        <!-- Control buttons-->        
    </div>
</template>

<script>

    import { extend } from 'vee-validate';
    import research from '../components/research.vue';
    import listaaw from '../components/listaaw.vue';
    import sindacoaw from '../components/sindacoaw.vue';
    import municipioaw from '../components/municipioaw.vue';
    export default {
        name: 'tabsaw',
        namespaced: true,
        components: {
            'app-research': research,
            'app-lista': listaaw,
            'app-sindaco': sindacoaw,
            'app-municipio': municipioaw
        },
        props: ['cat', 'tabIndex'],
        data: function () {
            return {
                form: {},
                show: true,
                messaggio: '',  
                tipo: '',  
                tabI: null
            }
        },
        mounted() {
            this.form = this.cat;
        }, 
        computed: {
            tab: {
                get() {
                    return parseInt(this.tabIndex);
                },
                set(value) {
                    this.tabI = value;
                }
            }
        },
        methods: {
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },           
            cercalista(e) {               
                this.form = e;              
                this.form.tipoInterrogazione = "1";
                this.$emit('sended', this.form, this.tabI);
            },
            cercasindaco(e) {
                this.form = e;
                this.form.tipoInterrogazione = "1";
                this.$emit('sended', this.form, this.tabI);
            },
            cercamunicipio(e) {             
                this.form = e;               
                this.form.tipoInterrogazione = "2";
                this.$emit('sended', this.form, this.tabI);
            },
            cercasezione(e) {
                this.form = e;               
                this.form.tipoInterrogazione = "3";
                this.$emit('sended', this.form, this.tabI);
            },
        }
    }

</script>

