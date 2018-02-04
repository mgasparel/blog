Vue.component('tag-input', {
    template: `
        <div>
            <div>
                <a v-for="tag in tags" href="" @click.prevent="del(tag)" class="tag__link">{{ tag }}</a>
            </div>

            <input type="hidden" v-for="(tag, i) in tags" :name="'Tags[' + i + ']'" :value="tag" />

            <input type="text" ref="textbox" placeholder="Tags" class="form-field__input" v-model="input" @keydown.tab.prevent="add" @keydown.enter.prevent="add">
        </div>
    `,

    data() {
        return {
            input: '',
            tags: []
        }
    },

    methods: {
        add() {
            let text = this.input.trim();

            if ( this.tags.indexOf(text) === -1 ){
                this.tags.push(text);
            }

            this.input = '';

            this.$refs.textbox.focus();
        },

        del(tag) {
            let index = this.tags.indexOf(tag)

            if ( index !== -1 ){
                this.tags.splice(index, 1);
            }

            this.$refs.textbox.focus();
        }
    }
});

new Vue({
    el: '.vue-tag-input'
})
