(function(){
  Vue.component('vdg-pager', {
    template: '#vdg-pager-template',
    props: { 
        
    }, 
    mounted: function () {
    
    },
    computed: {
 
     },
  });
   
  Vue.component('vdg', {
    template: '#vdg-template',
    mounted: function () {
       
      var self = this;
      var url = this.url;
      if (!url){
        throw new Error("cannot find \"data-url\" attribute")
      }
        
      this.$http.post(url, { page: self.page || 1, pageSize: self.pagesize }).then((response) => { 
        self.page = response.body.page;
        var x = response.body.rowCount / response.body.pageSize;
        for (var i = 0; i < (x); i++){
          self.pages.push({ number: (i+1) });
        }

        for (var key in response.body.cols) {
          if (response.body.cols.hasOwnProperty(key)){
            var col = response.body.cols[key];
            self.columns.push(col.text || col.key); 
          }
        }
        self.colspan = response.body.cols.length;
        
        for ( i = 0; i < response.body.rows.length; i++) {
          var row = response.body.rows[i]; 
          self.data.push(row);
        }
        // success callback
      }, (response) => {
        console.error(response);
      });
    },
    props: {
        url: String,
        glyphicon :String,
        pagesize: String
    },
    data: function () {
        var self = this;
      var sortOrders = {}
      //this.columns.forEach(function (key) {
      //  sortOrders[key] = null;
      //})
      return {
          page: 1,
          pages:[],
          columns:[],
          data:[],
          sortKey: '',
          colspan : 1,
          sortOrders: sortOrders,  
          sortOrderIconClass : function (key) {
            var direction = sortOrders[key];  
            var result = this.$el.attributes["data-classname-icon-sort-neutral"].value;
            if (direction !== undefined){
             result = direction > 0 ? this.$el.attributes["data-classname-icon-sort-up"].value : this.$el.attributes["data-classname-icon-sort-down"].value 
            }
            return result;
          } 
      }
    },
    computed: { 
    },
    filters: {
      capitalize: function (str) {
        return str.charAt(0).toUpperCase() + str.slice(1)
      }
    },
    methods: { 
        pagerClick: function (number, e) {
          var self = this;
          var url = this.url;
          if (!url) {
              throw new Error("cannot find \"data-url\" attribute")
          } 
            this.page = number;
          this.$http.post(url, { page: number || 1, pageSize: self.pagesize }).then((response) => {
              self.page = response.body.page;
              
              while (self.data.length > 0) { // empty current data
                  self.data.pop();
              }
              for (i = 0; i < response.body.rows.length; i++) {
                  var row = response.body.rows[i];
                  self.data.push(row);
              }
              // success callback
          }, (response) => {
              console.error(response);
          });
              e.stopPropagation();
          return false;
        }
    }
  });
   
  Vue.component('async-component', function (resolve, reject) {
      vue.$http.get('async-component.html', function(data, status, request){
          var parser = new DOMParser();
          var doc = parser.parseFromString(data, "text/html");
          resolve({
              template: doc
          });
      });
  });
   
  
})();