import { Component, AfterViewInit, ElementRef } from '@angular/core';

@Component({
  selector: 'app-get-car',
  imports: [],
  templateUrl: './get-car.html',
  styleUrl: './get-car.css',
})
export class GetCar implements AfterViewInit{
  constructor(private el:ElementRef)
  {}
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



