import { Component, ElementRef, Input, OnDestroy, ViewChild } from '@angular/core';
import { ProductImage } from '../../interfaces/product-image';
import { Carousel } from 'bootstrap';
@Component({
  selector: 'app-product-carousel-component',
  imports: [],
  templateUrl: './product-carousel-component.html',
  styleUrl: './product-carousel-component.css',
})
export class ProductCarouselComponent implements OnDestroy {
  @ViewChild('carouselEL') carouselEL !: ElementRef;
  @Input() productImages!: ProductImage[];


  private carousel: Carousel | null = null;

  startCarousel(): void{
    if(!this.carousel){

      this.carousel = new Carousel(this.carouselEL.nativeElement, {
        interval: 1500,
        ride: false
      });
    }
  }

  stopCarousel(): void{
    this.carousel?.pause();
  }

  ngOnDestroy(): void {
    this.carousel?.dispose();
  }
}
