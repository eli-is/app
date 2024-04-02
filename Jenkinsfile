pipeline {
    agent { label 'any' }
  
    tools {
        dotnetcore 'dotnet-sdk-8' //  you need a dotnet core tool installation defined in Jenkins
    }
  
    environment {
        APP_NAME = "app"
        RELEASE = "1.0.0"
        DOCKER_USER = "eli7890"
        DOCKER_PASS = 'dockerhub'
        IMAGE_NAME = "${DOCKER_USER}/${APP_NAME}"
        IMAGE_TAG = "${RELEASE}-${BUILD_NUMBER}"
    }
    
    stages {
        stage("Cleanup Workspace") {
            steps {
                cleanWs()
            }
        }

        stage("Checkout from SCM") {
            steps {
                git branch: 'main', credentialsId: 'github', url: 'https://github.com/eli-is/app.git'
            }
        }

        stage("Build Application") {
            steps {
                // Assuming your .NET Core project file is named 'app.csproj'
                sh 'dotnet restore app.csproj'
                sh 'dotnet build -c Release app.csproj'
            }
        }

        stage("Build & Push Docker Image") {
            steps {
                script {
                    // Build Docker image
                    docker.build("${IMAGE_NAME}:${IMAGE_TAG}")

                    // Push Docker image to Docker Hub
                    docker.withRegistry('https://index.docker.io/v1/', DOCKER_PASS) {
                        docker.image("${IMAGE_NAME}:${IMAGE_TAG}").push()
                        docker.image("${IMAGE_NAME}:latest").push()
                    }
                }
            }
        }

        stage("Deploy to Kubernetes") {
            steps {
                // Apply Kubernetes Deployment
                sh 'kubectl apply -f your-deployment.yaml -n app'
                
                // Apply Kubernetes Service
                sh 'kubectl apply -f your-service.yaml -n app'
                
                // Apply Kubernetes Ingress (if applicable)
                sh 'kubectl apply -f your-ingress.yaml -n app'
            }
        }
    }
}
