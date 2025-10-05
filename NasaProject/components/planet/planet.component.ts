import { Component, ElementRef, ViewChild, OnInit, OnDestroy } from '@angular/core';
import * as THREE from 'three';
// @ts-ignore
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';

@Component({
  selector: 'app-planet',
  standalone: true,
  templateUrl: './planet.component.html',
  styleUrls: ['./planet.component.css']
})
export class PlanetComponent implements OnInit, OnDestroy {
  @ViewChild('marsContainer', { static: true }) marsContainer!: ElementRef;

  private scene!: THREE.Scene;
  private camera!: THREE.PerspectiveCamera;
  private renderer!: THREE.WebGLRenderer;
  private controls!: OrbitControls;
  private mars!: THREE.Mesh;
  private markerMeshes: THREE.Mesh[] = [];

  private mouse = new THREE.Vector2();
  private raycaster = new THREE.Raycaster();

  private animationId: number | null = null;

  ngOnInit(): void {
    this.initThree();
    this.addLights();
    this.addMars();
    //this.addMarkers();
    this.addControls();
    this.animate();
    window.addEventListener('resize', this.onResize);
    this.renderer.domElement.addEventListener('click', this.onClick);
  }

  ngOnDestroy(): void {
    if (this.animationId) cancelAnimationFrame(this.animationId);
    window.removeEventListener('resize', this.onResize);
    this.renderer.domElement.removeEventListener('click', this.onClick);
  }

  private initThree(): void {
    const container = this.marsContainer.nativeElement;
    this.scene = new THREE.Scene();
    this.camera = new THREE.PerspectiveCamera(
      75,
      container.clientWidth / container.clientHeight,
      0.1,
      1000
    );
    this.camera.position.z = 3;

    this.renderer = new THREE.WebGLRenderer({ antialias: true });
    this.renderer.setSize(container.clientWidth, container.clientHeight);
    this.renderer.setPixelRatio(window.devicePixelRatio);

    container.appendChild(this.renderer.domElement);
  }

  private addLights(): void {
    const pointLight = new THREE.PointLight(0xffffff, 1);
    pointLight.position.set(2, 2, 2);

    const ambient = new THREE.AmbientLight(0xffffff, 0.3);

    this.scene.add(pointLight, ambient);
  }

  private addMars(): void {
    const geometry = new THREE.SphereGeometry(5, 256, 256);
    const textureLoader = new THREE.TextureLoader();
    const marsTexture = textureLoader.load('assets/mars.jpeg');
    const material = new THREE.MeshBasicMaterial({
      map: marsTexture,
      side: THREE.DoubleSide
    });

    this.mars = new THREE.Mesh(geometry, material);
    this.scene.add(this.mars);
  }
  // private addMarkers(): void {
  //   const markersData = [
  //     { lat: 18.65, lon: 226.2, title: 'Olympus Mons', description: 'Largest volcano in solar system' },
  //     { lat: -4.5, lon: 137.4, title: 'Gale Crater', description: 'Curiosity rover landing site' },
  //     { lat: 22.3, lon: 84.4, title: 'Valles Marineris', description: 'Huge canyon system' },
  //   ];
  //   const markerGroup = new THREE.Group();
  //   const markerMaterial = new THREE.MeshStandardMaterial({ emissive: 0xff5500, emissiveIntensity: 0.9 });
  //
  //   markersData.forEach(m => {
  //     const pos = this.latLonToVector3(m.lat, m.lon, 1.02);
  //     const geom = new THREE.SphereGeometry(0.03, 12, 12);
  //     const mesh = new THREE.Mesh(geom, markerMaterial.clone());
  //     mesh.position.copy(pos);
  //     (mesh as any).userData = m;
  //     markerGroup.add(mesh);
  //     this.markerMeshes.push(mesh);
  //   });
  //
  //   this.scene.add(markerGroup);
  // }

  private addControls(): void {
    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.controls.enableDamping = true;
    this.controls.dampingFactor = 0.05;
    this.controls.enableRotate = true;
    this.controls.enableZoom = true;
    this.controls.rotateSpeed = 0.1;
    this.controls.minDistance = 1.2;
    this.controls.maxDistance = 10;
  }
  private animate = () => {
    this.animationId = requestAnimationFrame(this.animate);
    this.mars.rotation.y += 0.002;
    this.controls.update();
    this.renderer.render(this.scene, this.camera);
  };

  private onResize = () => {
    const container = this.marsContainer.nativeElement;
    this.camera.aspect = container.clientWidth / container.clientHeight;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(container.clientWidth, container.clientHeight);
  };

  private onClick = (event: MouseEvent) => {
    const rect = this.renderer.domElement.getBoundingClientRect();
    this.mouse.x = ((event.clientX - rect.left) / rect.width) * 2 - 1;
    this.mouse.y = -((event.clientY - rect.top) / rect.height) * 2 + 1;
    this.raycaster.setFromCamera(this.mouse, this.camera);
    const intersects = this.raycaster.intersectObjects(this.markerMeshes, true);
    if (intersects.length > 0) {
      const obj: any = intersects[0].object;
      console.log('Clicked marker:', obj.userData);
    }
  };

  private latLonToVector3(lat: number, lon: number, r: number): THREE.Vector3 {
    const phi = (90 - lat) * (Math.PI / 180);
    const theta = (lon + 180) * (Math.PI / 180);

    const x = -r * Math.sin(phi) * Math.cos(theta);
    const z = r * Math.sin(phi) * Math.sin(theta);
    const y = r * Math.cos(phi);

    return new THREE.Vector3(x, y, z);
  }
}
