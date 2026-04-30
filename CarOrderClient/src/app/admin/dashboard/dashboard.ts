import { Component, OnInit } from '@angular/core';
import { Admin } from '../../services/admin';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit{

  data:any;
  constructor(private service:Admin)
  {}
  ngOnInit(): void {
   this.service.getDashBoard().subscribe(res=>{
    this.data=res;
   })

  }
  
}
