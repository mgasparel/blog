Vue.component('tag-input', {
    props: ['initialTags'],

    template: `
        <div class="tags-input">
            <div>
                <a v-for="tag in tags" href="" @click.prevent="del(tag)" class="tags-input__link">{{ tag }}</a>
            </div>

            <input type="hidden" v-for="(tag, i) in tags" :name="'Tags[' + i + ']'" :value="tag" />

            <input type="text" ref="textbox" placeholder="Tags" v-model="input" @keydown.tab.prevent="add" @keydown.enter.prevent="add">
        </div>
    `,

    data() {
        return {
            input: '',
            tags: JSON.parse(this.initialTags)
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
