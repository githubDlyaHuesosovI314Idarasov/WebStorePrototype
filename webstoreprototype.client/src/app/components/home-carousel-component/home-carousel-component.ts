import { Component, Input } from '@angular/core';
import { CarouselImage } from '../../interfaces/carousel-image';

@Component({
  selector: 'app-home-carousel-component',
  imports: [],
  templateUrl: './home-carousel-component.html',
  styleUrl: './home-carousel-component.css',
})
export class HomeCarouselComponent {
 @Input() caroselImages: CarouselImage[] = [];
}
