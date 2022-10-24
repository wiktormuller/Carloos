<script>
export default {
  // reactive state
  data() {
    return {
      getResult: null,
      postResult: null,
      putResult: null,
      deleteResult: null
    }
  },

  // functions that mutate state and trigger updates
  methods: {
    clearGetOutput() {
      this.getResult = null;
    },
    clearPostOutput() {
      this.postResult = null
    },
    clearPutOutput() {
      this.putResult = null;
    },
    clearDeleteOutput() {
      this.deleteResult = null;
    },
    formatResponse(res) {
      return JSON.stringify(res, null, 2);
    },
    async getEmploymentTypes() {
      try {
        const response = await fetch(`https://jobjet.azurewebsites.net/api/v1/employment-types`);
        if(!response.ok) {
          const message = `An error has occured: ${result.status} = ${result.statusText}`
          throw new Error(message);
        }

        const data = await response.json();

        const result = {
          status: response.status + "-" + response.statusText,
          headers: {
            "Content-Type": response.headers.get("Content-Type"),
            "Content-Length": response.headers.get("Content-Length")
          },
          length: response.headers.get("Content-Length"),
          data: data
        };
        this.getResult = this.formatResponse(result);
      }
      catch (err) {
        this.getResult = err.message;
      }
    },
    async getEmploymentTypeById() {
      const id = this.$refs.get_id.value;
      if(id) {
        try {
          // To get filtered data:
          // let url = new URL(`https://jobjet.azurewebsites.net/api/v1/employment-types`);
          // const params = { title: title };
          // url.search = new URLSearchParams(params);
          
          const response = await fetch(`https://jobjet.azurewebsites.net/api/v1/employment-types/${id}`)
          if(!response.ok) {
            const message = `An error has occured: ${response.status} - ${response.statusText}`;
            throw new Error(message);
          }
          const data = await response.json();
          const result = {
            data: data,
            status: response.status,
            statusText: response.statustext,
            headers: {
              "Content-Type": response.headers.get("Content-Type"),
              "Content-Length": response.headers.get("Content-Length")
            }
          };
          this.getResult = this.formatResponse(result);
        }
        catch (err) {
          this.getResult = err.message;
        }
      }
    },
    async createCompany() {
      const postData = {
        name: this.$refs.company_name.value,
        shortName: this.$refs.company_short_name.value,
        description: this.$refs.company_description.value,
        numberOfPeople: parseInt(this.$refs.company_number_of_people.value),
        cityName: this.$refs.company_city_name.value
      };
      
      try {
        const response = await fetch(`https://jobjet.azurewebsites.net/api/v1/companies`, {
          method: "post",
          headers: {
            "Content-Type": 'application/json; charset="utf-8"',
            "Accept": "text/plain",
            "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6IkNFTyIsImp0aSI6Ijk0MzQyMjcxLTA1YjItNGFjYi1iN2IxLWQ2ODA5Mjg3ZDk5YyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImVtYWlsIjoiY2VvQGpvYmpldC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIlVzZXIiXSwiZXhwIjoxNjUxNjE3NDUzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM5OC8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDM5OC8ifQ.ZCnM0nRG_bSIgV1PlYtgnZlMkhy2x7VS_f71b24o9xQ"
            //"x-access-token": "token-value"
          },
          body: JSON.stringify(postData)
        });
        
        console.log(JSON.stringify(postData));
        
        if(!response.ok) {
          const message = `An error has occured: ${response.status} - ${response.statusText}`;
          console.log(JSON.stringify(response));
          throw new Error(message);
        }
        
        const data = await response.json();
        const result = {
          status: response.status + "-" + response.statusText,
          headers: {
            "Content-Type": response.headers.get("Content-Type"),
            "Content-Length": response.headers.get("Content-Length")
          },
          data: data
        };
        this.postResult = this.formatResponse(result);
      }
      catch (err) {
        this.postResult = err.message;
      }
    },
    async editCompany() {
      const id = this.$refs.put_id.value;
      const putData = {
        description: this.$refs.put_description.value,
        numberOfPeople: parseInt(this.$refs.put_number_of_people.value)
      };
      
      try {
        const response = await fetch(`https://jobjet.azurewebsites.net/api/v1/companies/${id}`, {
          method: "put",
          headers: {
            "Content-Type": 'application/json; charset="utf-8"',
            "Accept": "*/*",
            "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6IkNFTyIsImp0aSI6Ijk0MzQyMjcxLTA1YjItNGFjYi1iN2IxLWQ2ODA5Mjg3ZDk5YyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImVtYWlsIjoiY2VvQGpvYmpldC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIlVzZXIiXSwiZXhwIjoxNjUxNjE3NDUzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM5OC8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDM5OC8ifQ.ZCnM0nRG_bSIgV1PlYtgnZlMkhy2x7VS_f71b24o9xQ"
            //"x-access-token": "token-value"
          },
          body: JSON.stringify(putData)
        });
        
        if(!response.ok) {
          const message = `An error has occured: ${response.status} - ${response.statusText}`;
          throw new Error(message);
        }
        
        //const data = await response.json();
        const result = {
          status: response.status + "-" + response.statusText,
          headers: {
            "Content-Type": response.headers.get("Content-Type")
          }//,
          //data: data
        };
        this.putResult = this.formatResponse(result);
      }
      catch (err) {
        this.putResult = err.message;
      }
    },
    async deleteCompanyById() {
  		const id = this.$refs.delete_id.value;
      if(id) {
        try {
          const response = await fetch(`https://jobjet.azurewebsites.net/api/v1/companies/${id}`, {
          method: "delete",
          headers: {
            "Content-Type": 'application/json; charset="utf-8"',
            "Accept": "*/*",
            "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6IkNFTyIsImp0aSI6Ijk0MzQyMjcxLTA1YjItNGFjYi1iN2IxLWQ2ODA5Mjg3ZDk5YyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImVtYWlsIjoiY2VvQGpvYmpldC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIlVzZXIiXSwiZXhwIjoxNjUxNjE3NDUzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM5OC8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDM5OC8ifQ.ZCnM0nRG_bSIgV1PlYtgnZlMkhy2x7VS_f71b24o9xQ"
            //"x-access-token": "token-value"
          }});
          
          if(!response.ok) {
          	const message = `An error has occured: ${response.status} - ${response.statusText}`;
          	throw new Error(message);
        	}
          
          //const data = await response.json();
        const result = {
            status: response.status + "-" + response.statusText,
            headers: {
              "Content-Type": response.headers.get("Content-Type")
            }//,
            //data: data
          };
          
          this.deleteResult = this.formatResponse(result);
        }
        catch (err) {
          this.deleteResult = null;
        }
      }
		}
  },

  // lifecycle hooks
  mounted() {
    
  }
}
</script>

<template>
  <div id="app" class="container">
    <div class="card">
      <div class="card-header">Employment Types</div>
      <div class="card-body">
        <div class="input-group input-group-sm">
          <button class="btn btn-sm btn-primary" @click="getEmploymentTypes">Get All</button>
          <br />
          <input type="text" ref="get_id" class="form-control ml-2" placeholder="Id" />
          <div class="input-group-append">
            <button class="btn btn-sm btn-primary" @click="getEmploymentTypeById">Get by Id</button>
          </div>
          <input type="text" ref="get_title" class="form-control ml-2" placeholder="Title" />
          <div class="input-group-append">
            <button class="btn btn-sm btn-primary" @click="getDataByTitle">Find By Title</button>
          </div>
          <button class="btn btn-sm btn-warning ml-2" @click="clearGetOutput">Clear</button>
        </div>   
        
        <div v-if="getResult" class="alert alert-secondary mt-2" role="alert"><pre>{{getResult}}</pre></div>
      </div>
    </div>
    <br />
    <br />
    <div class="card">
      <div class="card-header">Create Company</div>
      <div class="card-body">
        <div class="form-group">
          <input type="text" class="form-control" ref="company_name" placeholder="Name" />
        </div>
        <div class="form-group">
          <input type="text" class="form-control" ref="company_short_name" placeholder="Short Name" />
        </div>
        <div class="form-group">
          <input type="text" class="form-control" ref="company_description" placeholder="Description" />
        </div>
        <div class="form-group">
          <input type="text" class="form-control" ref="company_number_of_people" placeholder="Number of people" />
        </div>
        <div class="form-group">
          <input type="text" class="form-control" ref="company_city_name" placeholder="City name" />
        </div>
        <button class="btn btn-sm btn-primary" @click="createCompany">Create company</button>
        <button class="btn btn-sm btn-warning ml-2" @click="clearPostOutput">Clear</button>
        <div v-if="postResult" class="alert alert-secondary mt-2" role="alert"><pre>{{postResult}}</pre></div>
      </div>
    </div>
    <br />
    <br />
    <div class="card">
      <div class="card-header">Edit Company</div>
      <div class="card-body">
        <div class="form-group">
          <input type="text" class="form-control" ref="put_id" placeholder="Id" />
        </div>
        <div class="form-group">
          <input type="text" class="form-control" ref="put_description" placeholder="Description" />
        </div>
        <div class="form-group">
          <input type="text" class="form-control" ref="put_number_of_people" placeholder="Number of People" />
        </div>
        <button class="btn btn-sm btn-primary" @click="editCompany">Update Company Information</button>
        <button class="btn btn-sm btn-warning ml-2" @click="clearPutOutput">Clear</button>
        <div v-if="putResult" class="alert alert-secondary mt-2" role="alert"><pre>{{putResult}}</pre></div>
      </div>
    </div>
    <br />
    <br />
    <div class="card">
      <div class="card-header">Delete Company</div>
      <div class="card-body">
        <div class="input-group input-group-sm">
          <input type="text" ref="delete_id" class="form-control ml-2" placeholder="Id" />
          <div class="input-group-append">
            <button class="btn btn-sm btn-danger" @click="deleteCompanyById">Delete by Id</button>
          </div>
          <button class="btn btn-sm btn-warning ml-2" @click="clearDeleteOutput">Clear</button>
        </div>    
        
        <div v-if="deleteResult" class="alert alert-secondary mt-2" role="alert"><pre>{{deleteResult}}</pre></div>      
      </div>
    </div>
  </div>
</template>