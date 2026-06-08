import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CarService } from '../../../services/car-service';
import { Car } from '../../../models/car';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-details',
  imports: [CommonModule,RouterLink],
  templateUrl: './details.html',
  styleUrl: './details.css',
})
export class Details implements OnInit {
 constructor(private service:CarService,private route: ActivatedRoute)
  {}
    car:Car|null=null;
    carId!:number;
  ngOnInit(): void {
   this.route.paramMap.subscribe(params => {
    const id = params.get('id');
    if (id) {
      this.carId = +id;
      this.getCarbyId();
  }});
  }
  // getCarbyId() {
  //   this.service.getCarById(this.carId).subscribe({
  //   next: res => {
  //     this.car = {
  //       ...res,
  //       imageUrl: res.imageUrl?.startsWith('http')
  //         ? res.imageUrl
  //         : `${this.service.baseUrl}/images/${res.imageUrl}`
          
  //     };
      
  //   },
    
  //   error: err => {
  //     console.log('Error Details', err);
  //   }
  // });
  // }

  getCarbyId() {
  this.service.getCarById(this.carId).subscribe({
    next: res => {

      const imageUrl = res.imageUrl?.startsWith('http')
        ? res.imageUrl
        : `${this.service.baseUrl}/images/${res.imageUrl}`;

      this.car = {
        ...res,
        imageUrl
      };

      // Main image
      this.selectedImage = imageUrl;

      // Thumbnail images
      this.carImages = [
        imageUrl,
        imageUrl,
        imageUrl,
        imageUrl
      ];
    },
    error: err => {
      console.log('Error Details', err);
    }
  });
}
  isFavourite = false;

toggleFavourite() {
  this.isFavourite = !this.isFavourite;
}
selectedImage: string = '';

  zoomStyle = 'scale(1)';

  carImages: string[] = [];

  changeImage(img: string) {
    this.selectedImage = img;
  }

  onMouseMove(event: MouseEvent) {

    const target = event.target as HTMLElement;

    const rect = target.getBoundingClientRect();

    const x = ((event.clientX - rect.left) / rect.width) * 100;
    const y = ((event.clientY - rect.top) / rect.height) * 100;

    target.style.transformOrigin = `${x}% ${y}%`;

    this.zoomStyle = 'scale(2)';
  }

  onMouseLeave() {
    this.zoomStyle = 'scale(1)';
  }

 
}

 
  



  

  

  