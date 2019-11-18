import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  searchInput: string = "";
	searchResult: any;
	constructor() {}
  ngOnInit() {}

	search()
	{
		this.searchResult = [
				{ id: "1", description: "Desc1"},
				{ id: "2", description: "Desc2"}
			];	
  }
  
  // Returns object properties list
  getProperties = (obj) =>
  {
    return Object.keys(obj).map((key)=>{ return key});
 }
}
