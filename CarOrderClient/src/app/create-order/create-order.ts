import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CarService } from '../services/car-service';

@Component({
  selector: 'app-create-order',
  imports: [CommonModule,FormsModule],
  templateUrl: './create-order.html',
  styleUrl: './create-order.css',
})
export class CreateOrder implements OnInit {

  ngOnInit(): void {
   
  }

}
