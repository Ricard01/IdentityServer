import { Component, OnInit } from '@angular/core';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

claims: string[] | undefined;

  constructor(private homeService: HomeService) {  }
  ngOnInit(): void {
     this.homeService.getClaims().subscribe( (resp: any)=> {
      this.claims = resp
      console.log(this.claims)
     }
     )
    
    this.homeService.getUsers().subscribe(
      resp => console.log(resp)
    );
   
    
  }

}
