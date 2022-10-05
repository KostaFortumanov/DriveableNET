import {Component, OnInit} from '@angular/core';
import {PostService} from "../../services/post.service";
import {Post} from "../../models/post";
import {DomSanitizer} from "@angular/platform-browser";
import {FormBuilder, FormControl} from "@angular/forms";
import {mergeMap} from "rxjs";
import {PageEvent} from "@angular/material/paginator";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
    selector: 'app-posts',
    templateUrl: './posts.component.html',
    styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {

    posts: Post[] = [];
    images = new Map();
    manufacturers: string[] = []
    models: string[] = [];
    carTypes: string[] = []

    length!: number;
    pageIndex = 0;
    pageSize = 10;

    postsForm = this.formBuilder.group({
        manufacturer: '',
        model: '',
        yearFrom: '',
        yearTo: '',
        priceFrom: '',
        priceTo: '',
        mileageBellow: '',
        color: '',
        sortBy: 'date',
        carTypes: new FormControl([])
    })

    constructor(
        private postService: PostService,
        private formBuilder: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private sanitizer: DomSanitizer
    ) {
    }

    ngOnInit(): void {

        this.route.paramMap.subscribe({
            next: (data) => {
                this.pageIndex = +data.get('page')!;
                this.search()
            }
        })

        this.postService.getManufacturers().subscribe({
            next: (data) => {
                this.manufacturers = data
            }
        })

        this.postService.getCarTypes().subscribe({
            next: (data) => {
                this.carTypes = data
            }
        })

        this.postsForm.controls['manufacturer'].valueChanges.pipe(
            mergeMap((value) => this.postService.getModels(value))
        ).subscribe({
            next: (data) => {
                this.models = data
            }
        })

        this.postService.newPost.subscribe({
            next: () => {
                this.search();
            }
        })
    }

    search() {
        let manufacturer = this.postsForm.controls['manufacturer'].value
        let model = this.postsForm.controls['model'].value
        let yearFrom = this.postsForm.controls['yearFrom'].value
        let yearTo = this.postsForm.controls['yearTo'].value
        let priceFrom = this.postsForm.controls['priceFrom'].value
        let priceTo = this.postsForm.controls['priceTo'].value
        let mileageBellow = this.postsForm.controls['mileageBellow'].value
        let color = this.postsForm.controls['color'].value
        let sortBy = this.postsForm.controls['sortBy'].value
        let carTypes = this.postsForm.controls['carTypes'].value
        this.postService.getPosts(
            this.pageIndex,
            manufacturer,
            model,
            yearFrom,
            yearTo,
            priceFrom,
            priceTo,
            mileageBellow,
            color,
            sortBy,
            carTypes
        ).subscribe({
            next: (data) => {
                this.posts = data.postsResponse;
                this.length = data.size;
                this.getTitleImages();
            }
        })
    }

    reset(){
        this.postsForm = this.formBuilder.group({
            manufacturer: '',
            model: '',
            yearFrom: '',
            yearTo: '',
            priceFrom: '',
            priceTo: '',
            mileageBellow: '',
            color: '',
            sortBy: 'date',
            carTypes: new FormControl([])
        })
        this.search()

    }
    onPageEvent(pageEvent: PageEvent) {
        this.router.navigate(['posts', pageEvent.pageIndex + 1])
    }

    getTitleImages() {
        for (let post of this.posts) {
            this.postService.getPostImage(post.id, 0).subscribe({
                next: (data) => {
                    data.text().then((res: any) => {
                        let buff = new Buffer(res, 'base64');
                        let text = buff.toString('ascii');
                        this.images.set(post.id, text);
                    });
                }
            })
        }
    }
}
