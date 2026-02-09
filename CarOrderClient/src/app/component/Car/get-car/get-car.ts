import { Component, AfterViewInit, ElementRef, OnInit } from '@angular/core';
import { CarService } from '../../../services/car-service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Car } from '../../../models/car';

@Component({
  selector: 'app-get-car',
  imports: [CommonModule],
  templateUrl: './get-car.html',
  styleUrl: './get-car.css',
})
export class GetCar implements AfterViewInit , OnInit{
  constructor(private el:ElementRef,private service:CarService)
  {}
  

  car:Car[]=[];
  ngOnInit(): void {
    this.getList();
  }
  getList() {
    this.service.getCarView().subscribe((res:Car[]) => {
    
      this.car= res.map(c => ({
        ...c,
        imageUrl: c.imageUrl.startsWith('http')
          ? c.imageUrl
          : `${this.service.baseUrl}/images/${c.imageUrl}`
      }));
    });
  }

  

  onDeleteCar(selectCar:Car)
  {
    const isConfirm=confirm(`Do you want to delete this Car?`)
    if(isConfirm)
    {
      return this.service.deleteCar(selectCar.carId).subscribe(data=>{alert(`Delete successfull`)
        this.getList();
      });
      
    }
    return this.getList();
  }
  
  ngAfterViewInit(): void {
     const carousel = this.el.nativeElement.querySelector('.carousel');
    const list = carousel.querySelector('.list');
    const thumb = carousel.querySelector('.thumbnali');

    const nextBtn = carousel.querySelector('#next');
    const prevBtn = carousel.querySelector('#prev');

    nextBtn.addEventListener('click', () => this.showSlider('next', carousel, list, thumb));
    prevBtn.addEventListener('click', () => this.showSlider('prev', carousel, list, thumb));
  }
  showSlider(type: 'next' | 'prev', carousel: any, list: any, thumb: any) {
    const items = list.querySelectorAll('.item');
    const thumbs = thumb.querySelectorAll('.item');

    if (type === 'next') {
      carousel.classList.add('next');
      carousel.classList.remove('prev');

      list.appendChild(items[0]);
      thumb.appendChild(thumbs[0]);
    }

    if (type === 'prev') {
      carousel.classList.add('prev');
      carousel.classList.remove('next');

      list.prepend(items[items.length - 1]);
      thumb.prepend(thumbs[thumbs.length - 1]);
    }

    setTimeout(() => {
      carousel.classList.remove('next');
      carousel.classList.remove('prev');
    }, 500);
  }
}



