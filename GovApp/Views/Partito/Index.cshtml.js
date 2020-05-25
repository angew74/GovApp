import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import premier from '../../wwwroot/js/page/premier';

// funzionante con prop

/*new Vue({
    el: "#app",
    data: { codice: "Premier" },*/
    /*,beforeMount: function () {
        this.codice = "Premier";*/
  /*  },*/

   /* render: h => h(
        premier,
        {
            props: {
                codice: "Premier"
            }
        })
});*/

// funzionante senza prop



new Vue({
    el: "#app",   
    render: h => h(
     premier
    )
});