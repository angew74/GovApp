import cardsawVue from "./cardsaw.vue"
import Vue from "vue";
const components = [
    {
        component: cardsawVue,
        element: 'cards-aw'
    }
];

export default {
    loadComponents() {
        components.forEach(({ component, element }) => {
            // Is the custom element in the DOM?
            if (!document.querySelector(element)) {
                return;
            }

            // Create a new Vue instance and mount it to the custom element.
            new Vue({
                render: createElement => createElement(component)
            }).$mount(element);
        });
    }
}