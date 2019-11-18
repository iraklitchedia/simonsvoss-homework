import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  searchInput: string = "";
	searchResult: any = [];
	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }
  ngOnInit() {}

	search()
	{
    this.http.get<any>(this.baseUrl + 'api/Search/SearchText?text=' + this.searchInput).subscribe(result => {
      this.searchResult = result;
    }, error => window.alert("Something went wrong, please try again."));
  }
  
  // Returns object properties list
  getProperties = (obj) =>
  {
    return Object.keys(obj).map((key)=>{ return key});
 }
}
